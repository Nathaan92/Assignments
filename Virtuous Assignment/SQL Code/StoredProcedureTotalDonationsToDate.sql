CREATE PROCEDURE [DBO].[DonationTotalPerMonth]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT SUM(DonationCashValue)
	FROM dbo.DonationSummaries

END