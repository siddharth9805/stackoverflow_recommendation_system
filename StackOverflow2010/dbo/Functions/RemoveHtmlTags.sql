CREATE FUNCTION dbo.RemoveHtmlTags
(@Input NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @Start INT
    DECLARE @End INT
    DECLARE @Length INT

    WHILE CHARINDEX('<', @Input) > 0
    BEGIN
        SET @Start = CHARINDEX('<', @Input)
        SET @End = CHARINDEX('>', @Input, CHARINDEX('<', @Input))
        SET @Length = (@End - @Start) + 1

        IF @Start > 1
            SET @Input = STUFF(@Input, @Start, @Length, '')
        ELSE
            SET @Input = SUBSTRING(@Input, @Length + 1, LEN(@Input) - @Length)
    END

    RETURN LTRIM(RTRIM(@Input))
END