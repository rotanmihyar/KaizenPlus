using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using kaizenplus.Domain.Users;
using kaizenplus.Extensions;

namespace kaizenplus.Domain
{
    public class BaseEntity<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }

    public class CreationAuditedEntity<T> : BaseEntity<T>
    {
        public virtual Guid? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public virtual void SetCreator(Guid userId)
        {
            CreatedById = userId;
            CreatedDate = DateTimeExtension.SystemNow();
        }
    }

    public class AuditedEntity<T> : CreationAuditedEntity<T>
    {
        public virtual Guid? UpdatedById { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }

        public virtual void SetUpdater(Guid userId)
        {
            UpdatedById = userId;
            UpdatedDate = DateTimeExtension.SystemNow();
        }
    }

    public class NamedEntity<T> : BaseEntity<T>
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public string LocalizedName
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name.ToLower().StartsWith("ar"))
                {
                    return ArabicName;
                }
                return EnglishName;
            }
        }
    }

    public class CreationNamedEntity<T> : NamedEntity<T>
    {
        public virtual Guid? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public virtual void SetCreator(Guid userId)
        {
            CreatedById = userId;
            CreatedDate = DateTimeExtension.SystemNow();
        }
    }

    public class AuditedNamedEntity<T> : CreationNamedEntity<T>
    {
        public virtual Guid? UpdatedById { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }

        public virtual void SetUpdater(Guid userId)
        {
            UpdatedById = userId;
            UpdatedDate = DateTimeExtension.SystemNow();
        }
    }

    public class FullyAuditedEntity<T> : AuditedEntity<T>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Guid? DeletedById { get; set; }
        public User DeletedBy { get; set; }

        public virtual void Delete(Guid userId)
        {
            IsDeleted = true;
            DeletedById = userId;
            DeletionDate = DateTimeExtension.SystemNow();
        }
    }

    public class FullyAuditedNamedEntity<T> : AuditedNamedEntity<T>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }

        public Guid? DeletedById { get; set; }
        public User DeletedBy { get; set; }

        public virtual void Delete(Guid userId)
        {
            IsDeleted = true;
            DeletedById = userId;
            DeletionDate = DateTimeExtension.SystemNow();
        }
    }

    public abstract class BaseImageEntity<TPrimaryKey, TPrimaryEntity> : BaseEntity<long>
    {
        public string Name { get; set; }
        public string ContentType { get; set; }

        public TPrimaryKey ParentId { get; set; }
        public TPrimaryEntity Parent { get; set; }
    }
}