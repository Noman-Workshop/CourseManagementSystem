import os
import pyodbc
from dotenv import load_dotenv


def connect():
    load_dotenv()
    server = os.getenv("SERVER")
    database = os.getenv("DATABASE")
    user_id = os.getenv("USER_ID")
    password = os.getenv("PASSWORD")

    conn = pyodbc.connect(
        driver="{ODBC Driver 18 for SQL Server}",
        server=server,
        database=database,
        user=user_id,
        password=password,
        encrypt="no",
    )
    conn.autocommit = False
    cursor = conn.cursor()

    cursor.execute("SELECT @@version;")
    row = cursor.fetchone()
    while row:
        print(row[0])
        row = cursor.fetchone()

    return conn
