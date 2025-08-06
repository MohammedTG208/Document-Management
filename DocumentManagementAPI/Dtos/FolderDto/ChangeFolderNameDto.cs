namespace DocumentManagementAPI.Dtos.FolderDto
{
    /// <summary>
    /// DTO for changing the folder name and visibility status.
    /// </summary>
    public class ChangeFolderNameDto
    {
        /// <summary>
        /// Gets or sets the new name of the folder.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the folder is public.
        /// </summary>
        public required bool IsPublic { get; set; }
    }
}
