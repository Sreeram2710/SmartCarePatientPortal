# 🏥 SmartCare Patient Portal

A comprehensive web-based patient portal for healthcare management, built with ASP.NET Core MVC, Entity Framework, and Bootstrap.

![SmartCare Portal](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue) ![Entity Framework](https://img.shields.io/badge/Entity%20Framework-8.0-green) ![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple) ![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red)

## ✨ Features

### 👤 Multi-Role System
- **Admin**: User management, system reports, analytics
- **Doctor**: Patient management, appointments, medical records
- **Patient**: Appointment booking, medical history, prescriptions

### 🔐 Security & Authentication
- Secure user authentication with ASP.NET Identity
- Role-based access control
- HIPAA-compliant data handling
- Password strength requirements

### 📅 Appointment Management
- Online appointment booking
- Real-time availability checking
- Appointment status tracking
- Email notifications (configurable)

### 📋 Medical Records
- Electronic health records
- Prescription management
- Medical history tracking
- Secure data storage

### 📊 Dashboard & Analytics
- Role-specific dashboards
- Key performance indicators
- User activity tracking
- System usage reports

### 📱 Responsive Design
- Mobile-friendly interface
- Bootstrap 5 styling
- Cross-browser compatibility
- Accessibility features

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, jQuery, Font Awesome
- **Languages**: C#, HTML5, CSS3, JavaScript

## 🚀 Quick Start

### Prerequisites
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express/LocalDB/Full)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### 🏃‍♂️ Installation Steps

#### 1. Create Project
```bash
# Create new ASP.NET Core MVC project with Individual Accounts
dotnet new mvc --auth Individual -n SmartCarePatientPortal
cd SmartCarePatientPortal
```

#### 2. Install NuGet Packages
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
```

#### 3. Configure Database Connection
Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SmartCareDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### 4. Create Database
```bash
# In Package Manager Console
Add-Migration InitialCreate
Update-Database
```

#### 5. Run Application
```bash
dotnet run
```

Visit `https://localhost:5001` or `http://localhost:5000`

## 📁 Project Structure

```
SmartCarePatientPortal/
├── Controllers/
│   ├── HomeController.cs
│   ├── AccountController.cs
│   ├── AdminController.cs
│   ├── DoctorController.cs
│   ├── PatientController.cs
│   └── AppointmentController.cs
├── Models/
│   ├── ApplicationUser.cs
│   ├── Patient.cs
│   ├── Doctor.cs
│   ├── Appointment.cs
│   ├── MedicalRecord.cs
│   ├── Prescription.cs
│   ├── ApplicationDbContext.cs
│   └── ViewModels/
├── Views/
│   ├── Home/
│   ├── Account/
│   ├── Admin/
│   ├── Doctor/
│   ├── Patient/
│   ├── Appointment/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   ├── images/
│   └── lib/
├── Services/
├── Data/
└── Program.cs
```

## 👥 Demo Accounts

After running the application for the first time, use these demo accounts:

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@smartcare.com | Admin@123 |
| Doctor | doctor@smartcare.com | Doctor@123 |
| Patient | patient@smartcare.com | Patient@123 |

## 🎯 Key Functionalities

### For Patients
- ✅ Register and manage profile
- ✅ Book appointments with doctors
- ✅ View appointment history
- ✅ Access medical records
- ✅ View prescriptions
- ✅ Update personal information

### For Doctors
- ✅ View patient list
- ✅ Manage appointments
- ✅ Add medical records
- ✅ Update appointment status
- ✅ View patient medical history

### For Administrators
- ✅ Manage all users
- ✅ View system reports
- ✅ Monitor system usage
- ✅ Generate analytics

## 🔧 Configuration

### Email Settings (Optional)
Update `appsettings.json` to enable email notifications:
```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "your-email@gmail.com",
    "SmtpPassword": "your-app-password",
    "FromEmail": "noreply@smartcare.com",
    "FromName": "SmartCare Portal"
  }
}
```

### Security Settings
```json
{
  "SecuritySettings": {
    "RequireHttps": true,
    "EnableAuditLog": true,
    "SessionTimeoutMinutes": 30,
    "MaxLoginAttempts": 5,
    "LockoutDurationMinutes": 15
  }
}
```

## 🎨 UI Customization

### Color Themes
The application includes role-based color themes:
- **Admin**: Blue theme (`#667eea`)
- **Doctor**: Green theme (`#11998e`)
- **Patient**: Purple theme (`#764ba2`)

### Custom CSS
Modify `wwwroot/css/site.css` and `wwwroot/css/dashboard.css` for styling changes.

## 🚀 Deployment

### Local Deployment
1. Update connection string for your SQL Server instance
2. Run migrations: `Update-Database`
3. Publish: `dotnet publish -c Release`

### Azure Deployment
1. Create Azure App Service
2. Create Azure SQL Database
3. Update connection string in Azure configuration
4. Deploy using Visual Studio or Azure DevOps

### IIS Deployment
1. Install .NET 8 Hosting Bundle
2. Create IIS application
3. Update connection string
4. Configure application pool

## 🧪 Testing

### Demo Data
The application includes a database initializer that creates demo data:
- Sample admin, doctor, and patient accounts
- Example appointments and medical records

### Manual Testing Checklist
- [ ] User registration and login
- [ ] Role-based access control
- [ ] Appointment booking workflow
- [ ] Medical records management
- [ ] Dashboard functionality
- [ ] Responsive design on mobile

## 🔒 Security Features

- **Authentication**: ASP.NET Core Identity
- **Authorization**: Role-based access control
- **Data Protection**: Encrypted sensitive data
- **Session Management**: Secure session handling
- **CSRF Protection**: Anti-forgery tokens
- **Input Validation**: Server and client-side validation

## 📱 Browser Support

- ✅ Chrome 90+
- ✅ Firefox 88+
- ✅ Safari 14+
- ✅ Edge 90+
- ✅ Mobile browsers (iOS Safari, Chrome Mobile)

## 🐛 Troubleshooting

### Common Issues

**Database Connection Error**
- Verify SQL Server is running
- Check connection string format
- Ensure database exists

**Migration Issues**
- Delete Migrations folder and recreate
- Check for circular references in models
- Verify Entity Framework tools are installed

**Permission Denied**
- Check user roles are properly assigned
- Verify authentication is working
- Review authorization policies

**CSS/JS Not Loading**
- Check file paths in layout
- Verify files exist in wwwroot
- Clear browser cache

## 📈 Performance Optimization

- Use async/await for database operations
- Implement caching for frequently accessed data
- Optimize database queries with includes
- Compress static files
- Use CDN for external libraries

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support and questions:
- **Email**: support@smartcare.com
- **Documentation**: [Wiki](https://github.com/smartcare/wiki)
- **Issues**: [GitHub Issues](https://github.com/smartcare/issues)

## 🎖️ Acknowledgments

- **Bootstrap Team** for the UI framework
- **Microsoft** for ASP.NET Core and Entity Framework
- **Font Awesome** for icons
- **jQuery Team** for JavaScript library

## 📊 Project Status

- ✅ Core functionality complete
- ✅ Authentication and authorization
- ✅ Responsive design
- ✅ Database migrations
- ✅ Demo data seeding
- 🚧 Email notifications (optional)
- 🚧 Advanced reporting
- 🚧 Mobile app API

---

<div align="center">

**🏥 SmartCare Patient Portal - Revolutionizing Healthcare Management**

Made with ❤️ for better healthcare delivery

[Demo](https://smartcare-demo.azurewebsites.net) • [Documentation](https://docs.smartcare.com) • [Support](mailto:support@smartcare.com)

</div>
