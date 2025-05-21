namespace DocumentManagementAPI.Dtos.DocumentDto
{
    public class DocumentDownloadDto
    {
            public required byte[] DocData { get; set; }
            public required string ContentType { get; set; }
            public required string DocName { get; set; }
        

    }
}
