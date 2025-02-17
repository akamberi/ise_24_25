using BusPortal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.Domain.Models
{
    public class Booking
    {
       

public Guid Id { get; set; }
public required Client Client { get; set; }
public required Line Line { get; set; }
public DateTime DateTime { get; set; }
public required string Seat { get; set; }
public decimal Price { get; set; }

    }
}
