using HospitalTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalTests;

[TestClass]
public class PatientServiceTests
{
    private Mock<IPatientRepository>? _mockRepo;
    private Mock<INotificationService>? _mockNotification;
    private PatientService? _service;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<IPatientRepository>();
        _mockNotification = new Mock<INotificationService>();
        _service = new PatientService(_mockRepo.Object, _mockNotification.Object);
    }

    // === Write your tests and experiment with different Moq techniques ===

    [TestMethod]
    public async Task AdmitPatientAsync_CreatesPatientAndSendsNotification()
    {
        _mockRepo!.Setup(p => p.AddPatientAsync(new Patient { FullName = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 5, 4), IsAdmitted = true })).Returns(Task.CompletedTask);
        _mockNotification!.Setup(e => e.SendAdmissionNotificationAsync("john@example.com", "John Doe")).Returns(Task.CompletedTask);

        // Act
        int patientId = await _service!.AdmitPatientAsync("John Doe", "john@example.com", "555-1234", new DateTime(1990, 5, 15));

        // Assert
        Assert.IsTrue(patientId > -1); // first admitted patient will have Id = 0
        _mockRepo.Verify(p => p.AddPatientAsync(It.IsAny<Patient>()), Times.Once);
        _mockNotification.Verify(e => e.SendAdmissionNotificationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    [TestMethod]
    public async Task DiscartPateintAsync_Success_SendDischargeNotivicationAsync()
    {
        // Arrange
        var patient = new Patient
        {
            Id = 1,
            IsAdmitted = true
        };
        _mockRepo!.Setup(add => add.AddPatientAsync(patient)).Returns(Task.CompletedTask);
        _mockRepo.Setup(get => get.GetPatientAsync(patient.Id)).ReturnsAsync(patient);
        _mockRepo.Setup(u => u.UpdatePatientAsync(It.IsAny<Patient>())).Returns(Task.CompletedTask);
        _mockNotification!.Setup(e => e.SendDischargeNotificationAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        // Act
        await _service!.DischargePatientAsync(patient.Id);

        // Assert
        _mockNotification.Verify(e => e.SendDischargeNotificationAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    [TestMethod]
    [DataRow(1, false)] // Patient exists but not admitted
    [DataRow(2, false)] // Patient does not exist
    public async Task DischargePatientAsync_PatientNotFoundOrNotAdmitted_ThrowsException(int patientId, bool patientExists)
    {
        // Arrange
        Patient? patient = patientExists ? new Patient { Id = patientId, IsAdmitted = false } : null;
        _mockRepo!.Setup(get => get.GetPatientAsync(patientId)).ReturnsAsync(patient);

        // Act & Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(() => _service!.DischargePatientAsync(patientId));
    }
    [TestMethod]
    public async Task GetCurrentlyAdmittedPatientsAsync_ReturnsOnlyAdmittedPatients()
    {
        // Arrange
        var patients = new List<Patient>
        {
            new Patient { Id = 1, IsAdmitted = true },
            new Patient { Id = 2, IsAdmitted = false },
            new Patient { Id = 3, IsAdmitted = true }
        };
        _mockRepo!.Setup(p => p.GetAdmittedPatientsAsync()).ReturnsAsync(patients);

        // Act
        var result = await _service!.GetCurrentlyAdmittedPatientsAsync();

        // Assert
        var admitted = result.Where(p => p.IsAdmitted).ToList();
        Assert.AreEqual(2, admitted.Count);
        Assert.IsTrue(admitted.All(p => p.IsAdmitted));
    }
    [TestMethod]
    public async Task SendEmergencyAlertAsync_PatientExists_SendsCriticalAlert(){
        // Arrange
        var patient = new Patient { Id = 1, Phone = "555-1234" };
        _mockRepo!.Setup(get => get.GetPatientAsync(patient.Id)).ReturnsAsync(patient);
        _mockNotification!.Setup(e => e.SendCriticalAlertAsync(patient.Phone, "Emergency!")).Returns(Task.CompletedTask);

        // Act
        await _service!.SendEmergencyAlertAsync(patient.Id, "Emergency!");

        // Assert
        _mockNotification.Verify(e => e.SendCriticalAlertAsync(patient.Phone, "Emergency!"), Times.Once);
    }
}