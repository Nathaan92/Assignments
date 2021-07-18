CREATE PROCEDURE [DBO].[TotalDonationsToDate]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT SUM(DonationCashValue)
	FROM dbo.DonationSummaries

END