// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FSIS.App.Entities
{
    public partial class Player
    {
        public Player()
        {
            PlayerStats = new HashSet<PlayerStat>();
        }

        [Key]
        [Column("PlayerID")]
        public int PlayerId { get; set; }
        [Column("GuardianID")]
        public int GuardianId { get; set; }
        [Column("TeamID")]
        public int TeamId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Gender { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string AlbertaHealthCareNumber { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string MedicalAlertDetails { get; set; }
        public int GamesPlayed { get; set; }

        [ForeignKey("GuardianId")]
        [InverseProperty("Players")]
        public virtual Guardian Guardian { get; set; }
        [ForeignKey("TeamId")]
        [InverseProperty("Players")]
        public virtual Team Team { get; set; }
        [InverseProperty("Player")]
        public virtual ICollection<PlayerStat> PlayerStats { get; set; }
    }
}