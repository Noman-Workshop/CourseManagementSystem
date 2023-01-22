import os
import hashlib
import json
from faker import Faker

from util.connect import connect
from util.find import *
from util.generate import *
from util.insert import *
from util.log import *

fake = Faker()
Faker.seed(123)

NO_OF_USERS = 1000
USER_TO_EMPLOYEE_RATIO = 2 / 10
NO_OF_EMPLOYEES = int(NO_OF_USERS * USER_TO_EMPLOYEE_RATIO)
EMPLOYEE_HIERARCHY_BRANCHING_FACTOR = 3


def main(seed):
    conn = connect()

    # start a transaction
    cursor = conn.cursor()

    cursor.execute("BEGIN TRANSACTION")
    seed(cursor)
    cursor.execute("COMMIT TRANSACTION")

    # commit and close the connection
    conn.commit()
    conn.close()
