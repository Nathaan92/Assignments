CREATE PROCEDURE [DBO].[MonthlyDonationTotal]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT SUM(DonationCashValue) as Total, Month(DonationDate) as gmonth
	FROM dbo.DonationSummaries
	GROUP BY MONTH(DonationDate)

END