﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvDb.Application.Actions.Directors.DataTransferObjects;
public class DirectorDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Biography { get; set; }
    public string? PhotoLink { get; set; }
}