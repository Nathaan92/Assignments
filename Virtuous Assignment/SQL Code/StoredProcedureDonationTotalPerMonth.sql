CREATE PROCEDURE [DBO].[DonationTotalPerMonth]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT SUM(DonationCashValue)
	FROM dbo.DonationSummaries
	GROUP BY DATEPART(MONTH, DonationDate)
END