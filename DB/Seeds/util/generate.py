def generate_random_address(fake):
    _id = fake.uuid4()
    zip_code = fake.postcode()
    street = fake.street_address()
    house = fake.building_number()
    return _id, zip_code, street, house
