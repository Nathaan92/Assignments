CREATE PROCEDURE [DBO].[LargestDonationToDate]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT MAX(DonationCashValue)
	FROM dbo.DonationSummaries

END