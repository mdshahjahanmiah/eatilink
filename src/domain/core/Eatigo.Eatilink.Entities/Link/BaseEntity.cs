using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.Entities.Link
{
    public abstract class BaseEntity
    {
       public ObjectId Id { get; set; }
    }
}
