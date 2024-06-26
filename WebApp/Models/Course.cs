﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfessorId { get; set; }
        public User Professor { get; set; }
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}