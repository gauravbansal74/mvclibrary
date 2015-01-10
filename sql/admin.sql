Create table admin(
adminId BIGINT IDENTITY(1,1) PRIMARY KEY, 
adminFirstName VARCHAR(100),
adminLastName VARCHAR(100),
adminEmail VARCHAR(100) NOT NULL,
adminPassword VARCHAR(100) NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL
)

INSERT INTO admin (adminFirstName , adminLastName, adminEmail,adminPassword, createdOn, modifiedBy,modifiedOn)
 VAlUES('Gaurav','Kumar','gauravbansal74@gmail.com','admin123',GETDATE(),'1',GETDATE());