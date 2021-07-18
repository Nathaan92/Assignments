CREATE PROCEDURE [DBO].[DonationTotalPerMonth]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT MAX(DonationCashValue)
	FROM dbo.DonationSummaries

END