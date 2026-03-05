using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TestingMonitor.Domain.Exceptions;

namespace TestingMonitor.Domain.Enums;

/// <summary>
/// Error code.
/// </summary>
public enum ErrorCode
{
    [Display(Name = "Error with file saving.")]
    ErrorWithFileSaving = 0,

    #region Compiler

    [Display(Name = "Compiler error.")]
    CompilerStart = EntityCode.Compiler * 10000,

    [Display(Name = "Compiler already exists.")]
    CompilerAlreadyExists = CompilerStart + 1,

    [Display(Name = "Compiler not found.")]
    CompilerNotFound = CompilerStart + 2,

    [Display(Name = "Cannot delete a docker image.")]
    CannotDeleteImage = CompilerStart + 3,

    #endregion

    #region HeaderFile

    [Display(Name = "Header file error.")]
    HeaderFileStart = EntityCode.HeaderFile * 10000,

    [Display(Name = "Header file already exists.")]
    HeaderFileAlreadyExists = HeaderFileStart + 1,

    [Display(Name = "Header file not found.")]
    HeaderFileNotFound = HeaderFileStart + 2,

    #endregion

    #region Test

    [Display(Name = "Test error.")]
    TestStart = EntityCode.Test * 10000,

    [Display(Name = "Test already exists.")]
    TestAlreadyExists = TestStart + 1,

    [Display(Name = "Test not found.")]
    TestNotFound = TestStart + 2,

    #endregion

    #region TestGroup

    [Display(Name = "Test Group error.")]
    TestGroupStart = EntityCode.TestGroup * 10000,

    [Display(Name = "Test Group already exists.")]
    TestGroupAlreadyExists = TestGroupStart + 1,

    [Display(Name = "Test Group not found.")]
    TestGroupNotFound = TestGroupStart + 2,

    #endregion

    #region Compiler

    [Display(Name = "Compiler Task error.")]
    CompilerTaskStart = EntityCode.CompilerTask * 10000,

    [Display(Name = "Compiler Task already exists.")]
    CompilerTaskAlreadyExists = CompilerTaskStart + 1,

    [Display(Name = "Compiler Task not found.")]
    CompilerTaskNotFound = CompilerTaskStart + 2,

    #endregion
}

public enum EntityCode
{
    Reserved = 0,
    Compiler = 1,
    HeaderFile = 2,
    Test = 3,
    TestGroup = 4,
    CompilerTask = 5,
}


public static class ErrorCodeExtensions
{
    public static string GetDisplayName(this ErrorCode enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? enumValue.ToString();
    }

    public static void Throw(this ErrorCode error, string? message = null)
    {
        throw new ApiException(message == null ? error.GetDisplayName() : message);
    }
}