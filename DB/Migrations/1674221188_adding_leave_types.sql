alter table leave_types
    add no_of_days_allocated_per_year int not null;

alter table leave_types
    drop constraint uq_leave_types_short_name, uq_leave_types_full_name;

alter table leave_types
    add constraint uq_leave_types_description unique ([description]);

insert into leave_types(short_name, full_name, [description], gender_constraint, max_days_in_one_go,
                        no_of_times_redeemable, max_days_before_balance_reset, is_balance_forwarded,
                        is_real_time_balanced, is_pre_post_holiday_restricted, is_probation_period_restricted,
                        is_weekend_included, is_holiday_included, can_be_partially_allocated, is_late_adjusted,
                        is_absent_adjusted, is_paid, min_service_days, min_employee_level_rank, can_be_cashed,
                        review_forward_depth, no_of_days_allocated_per_year)
values ('EL', 'Earned Leave', 'Earned Leave', 'all', 100, 1000, 45, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 15),
       ('EL', 'Earned Leave', 'Earned Leave for employees from rank E-5 and 2 years service', 'all', 100, 1000, 60, 1,
        1, 0, 1, 0, 0, 1, 1, 1, 1, 730,
        70, 1, 1, 20),
       ('CL', 'Casual Leave', 'Casual Leave', 'all', 3, 1000, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 10),
       ('SL', 'Sick Leave', 'Sick Leave', 'all', 14, 1000, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 14),
       ('LWP', 'Leave Without Pay', 'Leave Without Pay', 'all', 100, 1000, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1,
        366),
       ('ML', 'Maternity Leave', 'Maternity Leave', 'female', 56, 4, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 180, 0, 0, 1, 0),
       ('PL', 'Paternity Leave', 'Paternity Leave', 'male', 3, 2, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0),
       ('SOL', 'Special/Other Leave', 'Other Leave', 'all', 100, 1000, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1,
        366);


insert into [migrations] ([version])
values ('1674221188_adding_leave_types');
