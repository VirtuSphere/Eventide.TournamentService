using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentService.ValueObjects.Exceptions;
/// <summary>
/// Исключение домена для случая, когда метод IsValid возвращает false, хотя метод Validate не бросил исключение
/// </summary>
internal class ValidationInconsistencyException() : Exception("Inconsistency between Validate and IsValid methods");