alter table employees
    add join_date date;

-- set join_date to created_at + 1 week
update employees
set join_date = DATEADD(week, 1, created_at);

-- add not null constraint
alter table employees
    add constraint employees_join_date_not_null
        check (join_date is not null);

insert into [migrations] ([version])
values ('1674243557_employee_joining');
