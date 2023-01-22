from setup import *

GENDERS = ()
ROLE_IDS = ()
HOLIDAYS = {}
EMPLOYEE_LEVELS = ()


def seed_users(n, cursor):
    for i in range(n):
        _id = fake.uuid4()
        first_name = fake.first_name()
        last_name = fake.last_name()
        # email is a combination of first and last name and first 5 digits of guid
        email = f"{first_name.lower()}.{last_name.lower()}.{_id[:5]}@cms.edu"
        # password is the last 5 digits of guid
        password = _id[-5:]
        # hash the password
        hashed_password = hashlib.sha512(password.encode('utf-8') + _id.encode('utf-8')).hexdigest()
        gender = fake.random_element(elements=GENDERS)
        created_at = fake.date_time_between(start_date='-10y', end_date='now')
        updated_at = fake.date_time_between(start_date=created_at, end_date='now')
        # generate a random address
        address_id, zip_code, street, house = generate_random_address(fake)

        print(address_id, zip_code, street, house, sep='|')
        print(_id, first_name, last_name, email, password, hashed_password,
              gender, created_at, updated_at, address_id, sep='|')
        print()

        # insert the address
        cursor.execute("INSERT INTO addresses "
                       "(id, zip_code, street, house, created_at, updated_at) "
                       "VALUES (?, ?, ?, ?, ?, ?)",
                       address_id, zip_code, street, house, created_at, updated_at)
        # insert the user
        cursor.execute(
            "INSERT INTO users "
            "(id, first_name, last_name, gender, email, password, address_id, created_at, updated_at) "
            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)",
            _id, first_name, last_name, gender, email, hashed_password, address_id, created_at, updated_at)

        # insert the user role
        role_id = fake.random_element(elements=ROLE_IDS)
        cursor.execute("INSERT INTO user_roles (user_id, role_id, created_at, updated_at) "
                       "VALUES (?, ?, ?, ?)",
                       _id, role_id, created_at, updated_at)


def seed_calender(cursor):
    year = datetime.now().year
    for year in range(year - 10, year + 2):
        # iterate over all the dates in the year
        for month in range(1, 13):
            for day in range(1, 32):
                date = None
                try:
                    date = datetime(year, month, day)
                except ValueError:
                    continue

                date_str = date.strftime('%Y-%m-%d')
                day_type = "workday"

                # check if the date is a weekend (Friday, Saturday)
                if date.weekday() in [4, 5]:
                    day_type = "weekend"

                # check if the date is a holiday
                if date_str in HOLIDAYS:
                    if HOLIDAYS[date_str]["primary_type"] == "Government Holiday":
                        day_type = "government_holiday"
                    else:
                        day_type = "other_holiday"

                # insert the date
                cursor.execute("INSERT INTO calender (day, type, created_at, updated_at) "
                               "VALUES (?, ?, ?, ?)",
                               date_str, day_type, date_str, date_str)


def seed_employees(n, hierarchy_branching_factor, cursor):
    users = list(find_n_random_user_ids(n, cursor))
    supervisors = [users[0]]

    _id = users[0]
    created_at = fake.date_time_between(start_date='-10y', end_date='now')
    updated_at = fake.date_time_between(start_date=created_at, end_date='now')
    level = fake.random_element(elements=EMPLOYEE_LEVELS)
    insert_employee({
        'id': _id,
        'level': level,
        'supervisor_id': None,
        'created_at': created_at,
        'updated_at': updated_at
    }, cursor)
    users.remove(_id)

    while supervisors and len(users) > 0:
        supervisor_id = supervisors.pop(0)
        no_of_subordinates = fake.random_int(min=0, max=hierarchy_branching_factor)
        subordinates = fake.random_elements(elements=users, length=min(len(users), no_of_subordinates), unique=True)
        for subordinate_id in subordinates:
            _id = subordinate_id
            created_at = fake.date_time_between(start_date='-10y', end_date='now')
            updated_at = fake.date_time_between(start_date=created_at, end_date='now')
            level = fake.random_element(elements=EMPLOYEE_LEVELS)
            insert_employee({
                'id': _id,
                'level': level,
                'supervisor_id': supervisor_id,
                'created_at': created_at,
                'updated_at': updated_at
            }, cursor)

            supervisors.append(subordinate_id)
            users.remove(subordinate_id)


def find(cursor):
    # find all genders
    global GENDERS
    GENDERS = find_genders(cursor)

    # find all role ids
    global ROLE_IDS
    ROLE_IDS = find_role_ids(cursor)

    # find all holidays
    global HOLIDAYS
    calenderific_api_key = os.getenv('CALENDERIFIC_API_KEY')
    HOLIDAYS = find_holidays(calenderific_api_key)

    global EMPLOYEE_LEVELS
    EMPLOYEE_LEVELS = find_employee_levels(cursor)


def seed(cursor):
    # find the constants
    find(cursor)

    seed_users(NO_OF_USERS, cursor)
    seed_calender(cursor)
    seed_employees(NO_OF_EMPLOYEES, EMPLOYEE_HIERARCHY_BRANCHING_FACTOR, cursor)


if __name__ == '__main__':
    main(seed)
