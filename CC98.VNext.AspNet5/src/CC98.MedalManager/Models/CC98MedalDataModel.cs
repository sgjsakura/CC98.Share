﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace CC98.MedalManager.Models
{
	/// <summary>
	/// 提供 CC98 勋章管理相关的数据模型。
	/// </summary>
	public class CC98MedalDataModel : DbContext
	{
		/// <summary>
		/// 获取数据库中所有勋章的集合。
		/// </summary>
		public virtual DbSet<Medal> Medals { get; set; }

		/// <summary>
		/// 获取数据库中所有勋章的分类的集合。
		/// </summary>
		public virtual DbSet<MedalCategory> MedalCategories { get; set; }

		/// <summary>
		/// 获取数据库中所有用户勋章颁发信息的集合。
		/// </summary>
		public virtual DbSet<UserMedalIssue> UserMedalIssues { get; set; }

		/// <summary>
		/// 获取数据库中所有用户勋章设置的集合。
		/// </summary>
		public virtual DbSet<UserMedalDisplaySetting> UserMedalDisplaySettings { get; set; }

		/// <summary>
		///     Override this method to further configure the model that was discovered by convention from the entity types
		///     exposed in <see cref="T:Microsoft.Data.Entity.DbSet`1" /> properties on your derived context. The resulting model may be cached
		///     and re-used for subsequent instances of your derived context.
		/// </summary>
		/// <remarks>
		///     If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.Data.Entity.DbContextOptionsBuilder.UseModel(Microsoft.Data.Entity.Metadata.IModel)" />)
		///     then this method will not be run.
		/// </remarks>
		/// <param name="modelBuilder">
		///     The builder being used to construct the model for this context. Databases (and other extensions) typically
		///     define extension methods on this object that allow you to configure aspects of the model that are specific
		///     to a given database.
		/// </param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// 配置复合键
			modelBuilder.Entity<UserMedalIssue>().HasKey(i => new { i.UserId, i.MedalId });
			modelBuilder.Entity<UserMedalDisplaySetting>().HasKey(i => new { i.UserId, i.MedalId });


			// 配置索引
			modelBuilder.Entity<UserMedalIssue>().HasIndex(i => i.IsUnderReview);
			modelBuilder.Entity<UserMedalDisplaySetting>().HasIndex(i => i.IsHide);
			modelBuilder.Entity<UserMedalDisplaySetting>().HasIndex(i => i.SortWeight);

		}
	}
}