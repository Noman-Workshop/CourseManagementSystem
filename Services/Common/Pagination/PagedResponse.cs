namespace CourseManagementSystem.ViewTables;

public class PagedResponse<T> where T : class {
	public int totalRecords { get; set; }
	public int pageSize { get; set; }
	public List<T> data { get; set; }
}
