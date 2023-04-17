-- for every employee in employees table
-- insert into leave_carried_remaining value

insert into leave_carried_remaining (employee_id, leave_type_short_name, carried_over, last_added_days, last_added_date,
                                     remaining_days)
select id,
       'EL',
       FLOOR(RAND(CHECKSUM(id)) * (15 - 5 + 1) + 1),
       15,
       '2023-01-01',
       FLOOR(RAND(CHECKSUM(id)) * (15 - 5 + 1) + 1)
from employees;

-- revert
-- delete from leave_carried_remaining;