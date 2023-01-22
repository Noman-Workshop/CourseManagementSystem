def insert_employee(employee, cursor):
    cursor.execute("INSERT INTO employees (id, [level], supervisor_id, created_at, updated_at) "
                   "VALUES (?, ?, ?, ?, ?)",
                   employee['id'], employee['level'], employee['supervisor_id'], employee['created_at'],
                   employee['updated_at'])
