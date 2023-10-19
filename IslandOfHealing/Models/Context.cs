using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IslandOfHealing.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticlesCategory> ArticlesCategories { get; set; }
        public virtual DbSet<ArticleComment> ArticleComments { get; set; }
        public virtual DbSet<CollectLike> CollectLikes { get; set; }
        public virtual DbSet<PricingPlan> PricingPlans { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<FollowWriters> FollowtWriters { get;  set; }
        public virtual DbSet<PaidArticleClicks> PaidArticleClicks { get;  set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ArticleContentImg> ArticleContentImgs { get; set; }
        public virtual DbSet<AIMessage> AIMessages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationSender> NotificationSenders { get; set; }
        public virtual DbSet<ConversationsCategory> ConversationsCategories { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<ConversationComment> ConversationComments { get; set; }
        public virtual DbSet<ConversationContentImg> ConversationContentImgs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
