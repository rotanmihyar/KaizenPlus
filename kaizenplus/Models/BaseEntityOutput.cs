using System;
using kaizenplus.Domain;

namespace kaizenplus.Models
{
    public class BaseEntityOutput<T>
    {
        public T Id { get; set; }

        public BaseEntityOutput(BaseEntity<T> entity)
        {
            if (entity != null)
            {
                Id = entity.Id;
            }
        }
    }

    public class CreationAuditedEntityOutput<T> : BaseEntityOutput<T>
    {
        public Guid? CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public CreationAuditedEntityOutput(CreationAuditedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                CreatedById = entity.CreatedById;
                CreatedDate = entity.CreatedDate;

                if (entity.CreatedBy != null)
                {
                    CreatedBy = entity.CreatedBy.GetFullName();
                }
            }
        }
    }

    public class AuditedEntityOutput<T> : CreationAuditedEntityOutput<T>
    {
        public Guid? UpdatedById { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public AuditedEntityOutput(AuditedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                UpdatedById = entity.UpdatedById;
                UpdatedDate = entity.UpdatedDate;

                if (entity.UpdatedBy != null)
                {
                    UpdatedBy = entity.UpdatedBy.GetFullName();
                }
            }
        }
    }

    public class NamedEntityOutput<T> : BaseEntityOutput<T>
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public NamedEntityOutput(NamedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                ArabicName = entity.ArabicName;
                EnglishName = entity.EnglishName;
            }
        }
    }

    public class CreationNamedEntityOutput<T> : NamedEntityOutput<T>
    {
        public Guid? CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public CreationNamedEntityOutput(CreationNamedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                CreatedById = entity.CreatedById;
                CreatedDate = entity.CreatedDate;

                if (entity.CreatedBy != null)
                {
                    CreatedBy = entity.CreatedBy.GetFullName();
                }
            }
        }
    }

    public class AuditedNamedEntityOutput<T> : CreationNamedEntityOutput<T>
    {
        public Guid? UpdatedById { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public AuditedNamedEntityOutput(AuditedNamedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                UpdatedById = entity.UpdatedById;
                UpdatedDate = entity.UpdatedDate;

                if (entity.UpdatedBy != null)
                {
                    UpdatedBy = entity.UpdatedBy.GetFullName();
                }
            }
        }
    }

    public class FullyAuditedEntityOutput<T> : AuditedEntityOutput<T>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Guid? DeletedById { get; set; }
        public string DeletedBy { get; set; }

        public FullyAuditedEntityOutput(FullyAuditedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                IsDeleted = entity.IsDeleted;
                DeletionDate = entity.DeletionDate;
                DeletedById = entity.DeletedById;

                if (entity.DeletedBy != null)
                {
                    DeletedBy = entity.DeletedBy.GetFullName();
                }
            }
        }
    }

    public class FullyAuditedNamedEntityOutput<T> : AuditedNamedEntityOutput<T>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Guid? DeletedById { get; set; }
        public string DeletedBy { get; set; }

        public FullyAuditedNamedEntityOutput(FullyAuditedNamedEntity<T> entity)
            : base(entity)
        {
            if (entity != null)
            {
                IsDeleted = entity.IsDeleted;
                DeletionDate = entity.DeletionDate;
                DeletedById = entity.DeletedById;

                if (entity.DeletedBy != null)
                {
                    DeletedBy = entity.DeletedBy.GetFullName();
                }
            }
        }
    }
}