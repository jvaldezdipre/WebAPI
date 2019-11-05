using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;

namespace BestTeamsApi.Models
{

    //Team and Player object to be able to create a database and relate them to eachother
    public class Team
    {
        public Team()
        {
            
            Players = new List<Player>();
            
        }
        
        //Primary Key
        public int ID { get; set; }
        public string TeamName { get; set; }
        public string Location { get; set; }

        //A collection of Players in the team
        public ICollection<Player> Players { get; set; } 
    }


    public class Player
    {

        //Primary Key
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Team Team { get; set; }

        //Foreign Key
        public int TeamID { get; set; }
    }

}