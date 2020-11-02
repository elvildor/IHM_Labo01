using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace PostIt.Model
{
    public class PostIt
    {
        public int Id { get; protected set; }

        public string Text { get; set; }

        public int ColorArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        [NotMapped]
        public Color Color { get; protected set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
