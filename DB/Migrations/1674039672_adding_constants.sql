-- gender
insert into [constants] (value, discriminator, created_at, updated_at)
values ('male', 'gender', default, default),
       ('female', 'gender', default, default);

-- calender day type
insert into [constants] (value, discriminator, created_at, updated_at)
values ('workday', 'calender_day_type', default, default),
       ('weekend', 'calender_day_type', default, default),
       ('government_holiday', 'calender_day_type', default, default),
       ('company_holiday', 'calender_day_type', default, default),
       ('other_holiday', 'calender_day_type', default, default);

-- employee level
insert into [constants] (value, discriminator, created_at, updated_at)
values ('trainee', 'employee_level', default, default),
       ('intern', 'employee_level', default, default),
       ('e-1', 'employee_level', default, default),
       ('e-2', 'employee_level', default, default),
       ('e-3', 'employee_level', default, default),
       ('e-4', 'employee_level', default, default),
       ('e-5', 'employee_level', default, default),
       ('e-6', 'employee_level', default, default),
       ('s-1', 'employee_level', default, default),
       ('s-2', 'employee_level', default, default),
       ('s-3', 'employee_level', default, default),
       ('s-4', 'employee_level', default, default),
       ('s-5', 'employee_level', default, default);

-- attendance status
insert into [constants] (value, discriminator, created_at, updated_at)
values ('absent', 'attendance_status', default, default),
       ('late', 'attendance_status', default, default),
       ('present', 'attendance_status', default, default),
       ('leave', 'attendance_status', default, default),
       ('weekend', 'attendance_status', default, default),
       ('holiday', 'attendance_status', default, default);

-- gender constraint
insert into [constants] (value, discriminator, created_at, updated_at)
values ('male', 'gender_constraint', default, default),
       ('female', 'gender_constraint', default, default),
       ('all', 'gender_constraint', default, default);

insert into [migrations] ([version])
values ('1674039672_adding_constants');
