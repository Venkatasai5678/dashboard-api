using Microsoft.EntityFrameworkCore;
using backend.MODEL;
using ModelEntity.MODEL;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Do NOT create City table — you already have it manually
        public DbSet<City> Cities { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<CustomerEdit> CustomerEdits { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        // Will create this new table
        public DbSet<Customer> CustomerDetails { get; set; }

        public DbSet<Contact> contacts { get; set; }
        public DbSet<ChartDataDto> ChartDataDto { get; set; }
        public DbSet<EmployeeName> EmployeeName { get; set; }
        public DbSet<EmployeeName> EmployeeName11 { get; set; }

        //public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }

        public DbSet<Appointment> Appointment { get; set; }
        //public DbSet<ApplicationLog> ApplicationLog { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(d => d.AppointmentId);


                entity.HasOne<Doctor>()
                      .WithMany()
                      .HasForeignKey(d => d.DoctorId)
                      .HasConstraintName("FK_Emp_Dept_Reference")
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
        /*
         This is an **excellent real-world architecture question** 👌
What you’re feeling now is *exactly* what every enterprise .NET dev feels the first time a system grows big.

Short answer first (to calm you down):

> ✅ **Yes, real-world projects often have ONE DbContext**
> ❌ **No, they do NOT dump 3,000 DbSets and all mappings in one giant file**

They use **organization patterns**. Let me explain **how this is handled in production systems**.

---

## 🔑 Big Picture (How real systems work)

* **1 database** → often **1 main DbContext**
* **Hundreds / thousands of tables**
* Code is split using:

  * Feature-based organization
  * Separate configuration classes
  * Partial DbContext
  * Sometimes multiple DbContexts (bounded contexts)

---

## 1️⃣ First misconception to fix

### ❌ Myth

> AppDbContext will become 10,000 lines long

### ✅ Reality

> AppDbContext stays **small and clean**

Because:

* DbSets are grouped
* Fluent API is moved out
* Context is split using **partial classes**

---

## 2️⃣ DbSet for all 3000 tables? ❌ NO

### 🔥 Important rule (INTERVIEW GOLD)

> **You do NOT need DbSet<T> for every table**

You need `DbSet<T>` **only if**:

* You query it directly
* You insert/update/delete it

If a table is:

* Lookup table
* Read-only
* Used only via joins or SPs

👉 **No DbSet required**

---

## 3️⃣ How AppDbContext looks in REAL projects

### 🔹 Core DbContext (SMALL)

```csharp
public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
```

👆 That’s it. Clean.

---

## 4️⃣ Where did 3000 table configs go? 🤔

👉 **Entity Configuration Classes**

---

## 5️⃣ One file per entity (REAL WORLD PATTERN)

### 📁 Folder structure

```
Data
 ├── AppDbContext.cs
 ├── Configurations
 │    ├── AppointmentConfig.cs
 │    ├── DoctorConfig.cs
 │    ├── PatientConfig.cs
 │    └── UserConfig.cs
```

---

### Example: `AppointmentConfig.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.AppointmentId);

        builder.HasOne<Doctor>()
               .WithMany()
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
```

---

### Example: `DoctorConfig.cs`

```csharp
public class DoctorConfig : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.DoctorId);
        builder.Property(d => d.Name).HasMaxLength(100);
    }
}
```

---

## 6️⃣ OnModelCreating becomes ONE LINE ⭐

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```

🔥 This is how **large enterprises do it**

---

## 7️⃣ Partial DbContext (OPTIONAL but COMMON)

Split DbContext into multiple files 👇

### AppDbContext.Core.cs

```csharp
public partial class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}
```

### AppDbContext.Appointments.cs

```csharp
public partial class AppDbContext
{
    public DbSet<Appointment> Appointments { get; set; }
}
```

✔ Same DbContext
✔ Clean separation
✔ Easy maintenance

---

## 8️⃣ When do companies use MULTIPLE DbContexts?

They do this when:

* Huge domain separation
* Different teams
* Different databases
* Microservices

Example:

* AuthDbContext
* BillingDbContext
* ReportingDbContext

---

## 9️⃣ Real Interview Answer (MEMORIZE THIS)

> In large enterprise systems, we typically use a single DbContext per bounded context.
> DbSet declarations are only added for entities that are directly accessed.
> Fluent API configurations are moved into separate IEntityTypeConfiguration classes and applied automatically in OnModelCreating, keeping the DbContext clean and maintainable.

🔥 This answer = **architect-level thinking**

---

## 🔟 Your current code – what to improve

### ❌ Problems

* Fluent API inside DbContext
* DTOs inside DbContext (`ChartDataDto`)
* Duplicate DbSet names
* Context will grow too large

### ✅ Fix

* Move configs to separate files
* Remove DTOs from DbContext
* Use `FromSqlRaw` for reports
* Use feature folders

---

## Final calm-down message 😄

You’re **thinking correctly**.
This confusion only comes when someone is moving from **small apps → real enterprise apps**.

You’re on the right path 💪

---

If you want next:

* Clean Architecture + DbContext placement
* How migrations work with many configs
* Handling legacy DB with 1000+ tables
* Performance tips with large DbContext

Just tell me 👍

         */

    }
}