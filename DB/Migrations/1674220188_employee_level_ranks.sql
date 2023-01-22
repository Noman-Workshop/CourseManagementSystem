create table [employee_levels]
(
    [level]       varchar(255) not null,
    [description] varchar(255) not null,
    [rank]        int          not null,
    [created_at]  datetime     not null default getdate(),
    [updated_at]  datetime     not null default getdate(),
    constraint [pk_employee_levels] primary key ([level]),
    constraint [uk_employee_levels_rank] unique ([rank])
);

insert into [employee_levels] (level, description, rank)
values ('trainee', 'trainee', 10),
       ('intern', 'intern', 20),
       ('e-1', 'e-1', 30),
       ('e-2', 'e-2', 40),
       ('e-3', 'e-3', 50),
       ('e-4', 'e-4', 60),
       ('e-5', 'e-5', 70),
       ('e-6', 'e-6', 80),
       ('s-1', 's-1', 90),
       ('s-2', 's-2', 100),
       ('s-3', 's-3', 110),
       ('s-4', 's-4', 120),
       ('s-5', 's-5', 130);

alter table employees
    drop constraint chk_employees_level;

alter table employees
    add constraint [fk_employees_level] foreign key ([level]) references [employee_levels] ([level]);

alter table [leave_types]
    drop constraint chk_leave_types_min_employee_level;

exec sp_rename 'leave_types.min_employee_level', 'min_employee_level_rank', 'COLUMN';

insert into [migrations] ([version])
values ('1674220188_employee_level_ranks');
