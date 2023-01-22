insert into [roles] ([name])
values ('SuperAdmin'),
       ('Admin'),
       ('Guest'),
       ('Anonymous'),
       ('Teacher'),
       ('TeacherAssistant'),
       ('Student'),
       ('Parent'),
       ('Principal'),
       ('VicePrincipal'),
       ('Counselor'),
       ('Librarian'),
       ('Accountant'),
       ('Registrar'),
       ('Professor'),
       ('AssociateProfessor'),
       ('AssistantProfessor'),
       ('Lecturer');

insert into [migrations] ([version])
values ('1674043737_adding_roles');
