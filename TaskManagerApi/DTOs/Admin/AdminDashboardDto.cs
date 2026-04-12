namespace TaskManagerApi.DTOs.Admin
{


    public class AdminDashboardDto
    {
        // AdminDashboardDto är inte kopplad till en enda entitet.
        // aggregations‑DTO, alltså en sammanställning av statistik.

        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }

        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PublicTasks { get; set; }

        public int TotalComments { get; set; }
    }
}
