﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CostumerDto
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public byte MembershipTypeId { get; set; }
        public MembershipTypeDto MembershipType { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}