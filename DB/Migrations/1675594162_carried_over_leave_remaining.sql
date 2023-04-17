create table [leave_carried_remaining]
(
    [employee_id]           uniqueidentifier not null,
    constraint [fk_leave_carried_remaining_employee_id]
        foreign key ([employee_id]) references [employees] ([id]),

    [leave_type_short_name] varchar(100)     not null,

    constraint [pk_leave_carried_remaining]
        primary key ([employee_id], [leave_type_short_name]),

    [carried_over]          int              not null,
    [last_added_days]       int              not null,
    [last_added_date]       datetime         not null,
    [remaining_days]        int              not null,
    [created_at]            datetime         not null default getdate(),
    [updated_at]            datetime         not null default getdate(),
);

insert into [migrations] ([version])
values ('1675594162_carried_over_leave_remaining');

-- revert
-- delete
-- from [migrations]
-- where [version] = '1675594162_carried_over_leave_remaining';
-- drop table [leave_carried_remaining];
