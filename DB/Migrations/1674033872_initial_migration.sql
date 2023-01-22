-- db: sql server

create table [addresses]
(
    [id]         uniqueidentifier not null default newid(),
    constraint pk_addresses
        primary key ([id]),

    [zip_code]   varchar(5)       not null,
    [street]     varchar(100)     not null,
    [house]      varchar(100)     not null,
    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table [constants]
(
    [id]            int          not null identity (1, 1),
    constraint pk_constants
        primary key ([id]),
    [value]         varchar(255) not null,
    [discriminator] varchar(255) not null,
    [created_at]    datetime     not null default getdate(),
    [updated_at]    datetime     not null default getdate(),

    constraint uq_constants_value_discriminator
        unique ([value], [discriminator]),
);

create function [fn_constant_exists](@value varchar(255), @discriminator varchar(255))
    returns int
as
begin
    declare @result int
    select @result =
           IIF(exists(select 1 from [constants] where [value] = @value and [discriminator] = @discriminator), 1, 0)
    return @result
end;

create table [users]
(
    [id]         uniqueidentifier not null default newid(),
    constraint pk_users
        primary key ([id]),

    [first_name] varchar(256)     not null,
    [last_name]  varchar(256)     not null,

    [email]      varchar(256)     not null,
    constraint uq_users_email
        unique ([email]),

    [password]   varchar(512)     not null,

    [gender]     varchar(255)     not null,
    constraint chk_users_gender
        check ([dbo].fn_constant_exists([gender], 'gender') = 1),

    [address_id] uniqueidentifier not null,
    constraint fk_users_address_id
        foreign key ([address_id]) references [addresses] ([id]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table [roles]
(
    [id]         uniqueidentifier not null default newid(),
    constraint pk_roles
        primary key ([id]),

    [name]       varchar(100)     not null,
    constraint uq_roles_name
        unique ([name]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table user_roles
(
    [user_id]    uniqueidentifier not null,
    constraint fk_user_roles_user_id
        foreign key ([user_id]) references [users] ([id]),

    [role_id]   uniqueidentifier not null,
    constraint fk_user_roles_role_id
        foreign key ([role_id]) references [roles] ([id]),

    constraint pk_user_roles
        primary key ([user_id], [role_id]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table [calender]
(
    [day]        date         not null,
    constraint pk_calender
        primary key ([day]),

    [type]       varchar(255) not null,
    constraint chk_calender_type
        check ([dbo].fn_constant_exists([type], 'calender_day_type') = 1),

    [created_at] datetime     not null default getdate(),
    [updated_at] datetime     not null default getdate(),
);

create table [employees]
(
    [id]            uniqueidentifier not null,
    constraint fk_employee_id
        foreign key ([id]) references [users] ([id]),
    constraint pk_employees
        primary key ([id]),

    [level]         varchar(255)     not null,
    constraint chk_employees_level
        check ([dbo].fn_constant_exists([level], 'employee_level') = 1),

    [supervisor_id] uniqueidentifier null,
    constraint fk_employees_supervisor_id
        foreign key ([supervisor_id]) references [employees] ([id]),

    [created_at]    datetime         not null default getdate(),
    [updated_at]    datetime         not null default getdate(),
);

create table [employee_attendances]
(
    [date]        date             not null,
    constraint fk_employee_attendances_date
        foreign key ([date]) references [calender] ([day]),

    [employee_id] uniqueidentifier not null,
    constraint fk_employee_attendances_employee_id
        foreign key ([employee_id]) references [employees] ([id]),

    [status]      varchar(255)     not null,
    constraint chk_employee_attendances_status
        check ([dbo].fn_constant_exists([status], 'attendance_status') = 1),

    [in_time]     time             not null,
    [out_time]    time             not null,
    [remarks]     varchar(1000)    null,
    [created_at]  datetime         not null default getdate(),
    [updated_at]  datetime         not null default getdate(),
);

create table [agenda]
(
    [id]          uniqueidentifier not null default newid(),
    constraint pk_agenda
        primary key ([id]),

    [title]       varchar(100)     not null,

    [date]        date             not null,
    constraint fk_agenda_date
        foreign key ([date]) references [calender] ([day]),

    [description] varchar(1000)    null,

    [address_id]  uniqueidentifier not null,
    constraint fk_agenda_address_id
        foreign key ([address_id]) references [addresses] ([id]),

    [start_time]  time             not null,
    [end_time]    time             not null,
    [created_at]  datetime         not null default getdate(),
    [updated_at]  datetime         not null default getdate(),
);

create table [leave_types]
(
    [id]                             uniqueidentifier not null default newid(),
    constraint pk_leave_types
        primary key ([id]),

    [short_name]                     varchar(100)     not null,
    constraint uq_leave_types_short_name
        unique ([short_name]),

    [full_name]                      varchar(100)     not null,
    constraint uq_leave_types_full_name
        unique ([full_name]),

    [description]                    varchar(1000)    not null,
    [gender_constraint]              varchar(255)     not null,
    constraint chk_leave_types_gender_constraint
        check ([dbo].fn_constant_exists([gender_constraint], 'gender_constraint') = 1),

    [max_days_in_one_go]             int              not null,
    [no_of_times_redeemable]         int              not null,
    [max_days_before_balance_reset]  int              not null,
    [is_balance_forwarded]           bit              not null,
    [is_real_time_balanced]          bit              not null,
    [is_pre_post_holiday_restricted] bit              not null,
    [is_probation_period_restricted] bit              not null,
    [is_weekend_included]            bit              not null,
    [is_holiday_included]            bit              not null,
    [can_be_partially_allocated]     bit              not null,
    [is_late_adjusted]               bit              not null,
    [is_absent_adjusted]             bit              not null,
    [is_paid]                        bit              not null,
    [min_service_days]               int              not null,

    [min_employee_level]             int              not null,
    constraint chk_leave_types_min_employee_level
        check ([dbo].fn_constant_exists([min_employee_level], 'employee_level') = 1),

    [can_be_cashed]                  bit              not null,
    [review_forward_depth]           int              not null,
    [created_at]                     datetime         not null default getdate(),
    [updated_at]                     datetime         not null default getdate(),
);

create table [leave_requests]
(
    [id]                  uniqueidentifier not null default newid(),
    constraint pk_leave_requests
        primary key ([id]),

    [employee_id]         uniqueidentifier not null,
    constraint fk_leave_requests_employee_id
        foreign key ([employee_id]) references [users] ([id]),

    [type]                uniqueidentifier not null,
    constraint fk_leave_requests_type_id
        foreign key ([type]) references [leave_types] ([id]),

    [proposed_start_date] date             not null,
    [proposed_end_date]   date             not null,
    [proposed_days]       int              not null,
    [actual_start_date]   date             not null,
    [actual_end_date]     date             not null,
    [actual_days]         int              not null,
    [purpose]             varchar(1000)    not null,

    [address_id]          uniqueidentifier not null,
    constraint fk_leave_requests_leave_address_id
        foreign key ([address_id]) references [addresses] ([id]),

    [is_approved]         bit              not null,
    [cancelled_date]      date             null,
    [cancelled_reason]    varchar(1000)    null,
    [reliever_id]         uniqueidentifier null,
    constraint fk_leave_requests_reliever_id
        foreign key ([reliever_id]) references [employees] ([id]),

    [created_at]          datetime         not null default getdate(),
    [updated_at]          datetime         not null default getdate(),
);

create table [leave_request_reviews]
(
    [id]                   uniqueidentifier not null default newid(),
    constraint pk_leave_request_reviews
        primary key ([id]),

    [request_id]           uniqueidentifier not null,
    constraint fk_leave_request_reviews_request_id
        foreign key ([request_id]) references [leave_requests] ([id]),

    [reviewer_id]          uniqueidentifier not null,
    constraint fk_leave_request_reviews_reviewer_id
        foreign key ([reviewer_id]) references [employees] ([id]),

    [order]                int              not null,
    [comment]              varchar(1000)    null,
    [is_approved]          bit              not null,
    [permitted_start_date] date             not null,
    [permitted_end_date]   date             not null,
    [permitted_days]       int              not null,

    [reliever_id]          uniqueidentifier null,
    constraint fk_leave_request_reviews_reliever_id
        foreign key ([reliever_id]) references [employees] ([id]),

    [created_at]           datetime         not null default getdate(),
    [updated_at]           datetime         not null default getdate(),
);

create table [departments]
(
    [id]         uniqueidentifier not null default newid(),
    constraint pk_departments
        primary key ([id]),

    [name]       varchar(100)     not null,
    constraint uq_departments_name
        unique ([name]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table [teachers]
(
    [id]            uniqueidentifier not null,
    constraint fk_teacher_id
        foreign key ([id]) references [employees] ([id]),
    constraint pk_teachers
        primary key ([id]),

    [department_id] uniqueidentifier not null,
    constraint fk_teachers_department_id
        foreign key ([department_id]) references [departments] ([id]),

    [created_at]    datetime         not null default getdate(),
    [updated_at]    datetime         not null default getdate(),
);

create table [department_heads]
(
    [teacher_id]    uniqueidentifier not null,
    constraint fk_department_heads_teacher_id
        foreign key ([teacher_id]) references [teachers] ([id]),

    [department_id] uniqueidentifier not null,
    constraint fk_department_heads_department_id
        foreign key (department_id) references [departments] ([id]),

    constraint pk_department_heads
        primary key ([teacher_id], department_id),

    [created_at]    datetime         not null default getdate(),
    [updated_at]    datetime         not null default getdate(),
);

create table [courses]
(
    [id]            uniqueidentifier not null default newid(),
    constraint pk_courses
        primary key ([id]),

    [name]          varchar(100)     not null,
    constraint uq_courses_name
        unique ([name]),

    [description]   varchar(1000)    not null,
    [credits]       decimal          not null,

    [department_id] uniqueidentifier not null,
    constraint fk_courses_department_id
        foreign key ([department_id]) references [departments] ([id]),

    [created_at]    datetime         not null default getdate(),
    [updated_at]    datetime         not null default getdate(),
);

create table [classes]
(
    [id]          uniqueidentifier not null default newid(),
    constraint pk_classes
        primary key ([id]),

    [name]        varchar(100)     not null,
    [description] varchar(1000)    not null,

    [course_id]   uniqueidentifier not null,
    constraint fk_classes_course_id
        foreign key ([course_id]) references [courses] ([id]),

    [created_at]  datetime         not null default getdate(),
    [updated_at]  datetime         not null default getdate(),
);

create table [class_teachers]
(
    [teacher_id] uniqueidentifier not null,
    constraint fk_class_teachers_id
        foreign key ([teacher_id]) references [teachers] ([id]),

    [class_id]   uniqueidentifier not null,
    constraint fk_class_teachers_class_id
        foreign key ([class_id]) references [classes] ([id]),

    constraint pk_class_teachers
        primary key ([teacher_id], [class_id]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table [students]
(
    [id]         uniqueidentifier not null,
    constraint fk_students_id
        foreign key ([id]) references [users] ([id]),
    constraint pk_students
        primary key ([id]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table [enrollments]
(
    [id]         uniqueidentifier not null default newid(),
    constraint pk_enrollments
        primary key ([id]),

    [student_id] uniqueidentifier not null,
    constraint fk_enrollments_student_id
        foreign key ([student_id]) references [students] ([id]),

    [class_id]   uniqueidentifier not null,
    constraint fk_enrollments_class_id
        foreign key ([class_id]) references [classes] ([id]),

    [grade]      decimal          null,

    constraint uq_enrollments_student_class
        unique ([student_id], [class_id]),

    [created_at] datetime         not null default getdate(),
    [updated_at] datetime         not null default getdate(),
);

create table migrations
(
    [id]         int          not null identity (1,1),
    constraint pk_migrations
        primary key ([id]),

    [version]    varchar(100) not null,
    constraint uq_migrations_version
        unique ([version]),

    [created_at] datetime     not null default getdate(),
    [updated_at] datetime     not null default getdate(),
);

insert into [migrations] ([version])
values ('1674033872_initial_migration');