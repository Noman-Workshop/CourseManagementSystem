from datetime import datetime

import requests as requests


def find_genders(cursor):
    cursor.execute("SELECT [value] from constants where discriminator = 'gender'")
    genders = cursor.fetchall()
    return tuple(gender[0] for gender in genders)


def find_role_ids(cursor):
    cursor.execute("""
        SELECT id FROM roles
        where [name] not in 
        ('SuperAdmin', 'Admin', 'Guest', 'Anonymous',
        'Principal', 'VicePrincipal', 'Registrar')
    """)
    role_ids = cursor.fetchall()
    return tuple(role_id[0] for role_id in role_ids)


def find_holidays(calenderific_api_key):
    holidays = {}
    url = "https://calendarific.com/api/v2/holidays"
    year = datetime.now().year
    for year in range(year - 10, year + 2):
        print('Finding holidays for year', year)
        params = {
            "api_key": calenderific_api_key,
            "country": "BD",
            "year": year
        }
        response = requests.get(url, params=params)
        holiday_in_years = response.json()
        for holiday in holiday_in_years['response']['holidays']:
            holidays[holiday['date']['iso'][0:10]] = holiday
    return holidays


def find_n_random_user_ids(n, cursor):
    cursor.execute("SELECT id FROM users ORDER BY NEWID() OFFSET 0 ROWS FETCH NEXT ? ROWS ONLY", n)
    user_ids = cursor.fetchall()
    return tuple(user_id[0] for user_id in user_ids)


def find_employee_levels(cursor):
    cursor.execute("SELECT [value] from constants where discriminator = 'employee_level'")
    employee_levels = cursor.fetchall()
    return tuple(employee_level[0] for employee_level in employee_levels)


def find_all_employees(cursor):
    cursor.execute("""
        SELECT id, [level], supervisor_id, created_at, updated_at, join_date
        FROM employees
    """)
    employees_raw = cursor.fetchall()
    employees = {}
    for employee in employees_raw:
        employees[employee[0]] = {
            'id': employee[0],
            'level': employee[1],
            'supervisor_id': employee[2],
            'created_at': employee[3],
            'updated_at': employee[4],
            'join_date': employee[5]
        }
    return employees


def find_leave_types(cursor):
    cursor.execute("""
        SELECT id, short_name, full_name, [description], gender_constraint, max_days_in_one_go, no_of_times_redeemable,
        max_days_before_balance_reset, is_balance_forwarded, is_real_time_balanced, is_pre_post_holiday_restricted,
        is_probation_period_restricted, is_weekend_included, is_holiday_included, can_be_partially_allocated,
        is_late_adjusted, is_absent_adjusted, is_paid, min_service_days, min_employee_level_rank, can_be_cashed,
        review_forward_depth
        from [leave_types]
    """)
    leave_types_raw = cursor.fetchall()
    leave_types = {}
    for leave_type in leave_types_raw:
        leave_types[leave_type[0]] = {
            'id': leave_type[0],
            'short_name': leave_type[1],
            'full_name': leave_type[2],
            'description': leave_type[3],
            'gender_constraint': leave_type[4],
            'max_days_in_one_go': leave_type[5],
            'no_of_times_redeemable': leave_type[6],
            'max_days_before_balance_reset': leave_type[7],
            'is_balance_forwarded': leave_type[8],
            'is_real_time_balanced': leave_type[9],
            'is_pre_post_holiday_restricted': leave_type[10],
            'is_probation_period_restricted': leave_type[11],
            'is_weekend_included': leave_type[12],
            'is_holiday_included': leave_type[13],
            'can_be_partially_allocated': leave_type[14],
            'is_late_adjusted': leave_type[15],
            'is_absent_adjusted': leave_type[16],
            'is_paid': leave_type[17],
            'min_service_days': leave_type[18],
            'min_employee_level_rank': leave_type[19],
            'can_be_cashed': leave_type[20],
            'review_forward_depth': leave_type[21],
        }

    return leave_types
