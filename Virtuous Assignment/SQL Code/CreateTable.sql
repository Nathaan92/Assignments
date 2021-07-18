CREATE Table DonationSummary(
	ID int NOT NULL IDENTITY PRIMARY KEY,
	DonorsName varchar(225) NULL,
	Email varchar(225) NULL,
	DonationDate DateTime NOT NULL,
	DonationType varchar (30) NOT NULL,
	DonationCashValue decimal NOT NULL
);
