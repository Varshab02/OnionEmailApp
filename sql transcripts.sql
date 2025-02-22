use OnionEmailDb;


CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO Users (Name, Email) 
VALUES 
('Varsha Bennuri', 'bennuri_varsha@epam.com');


INSERT INTO Users (Name, Email) 
VALUES 
('Likitha', 'samayamanthula_likhita@epam.com');


Select * from Users;