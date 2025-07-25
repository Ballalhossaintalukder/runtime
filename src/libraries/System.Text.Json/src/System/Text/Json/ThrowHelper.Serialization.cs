// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Pipelines;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json
{
    internal static partial class ThrowHelper
    {
        [DoesNotReturn]
        public static void ThrowArgumentException_DeserializeWrongType(Type type, object value)
        {
            throw new ArgumentException(SR.Format(SR.DeserializeWrongType, type, value.GetType()));
        }

        [DoesNotReturn]
        public static void ThrowArgumentException_SerializerDoesNotSupportComments(string paramName)
        {
            throw new ArgumentException(SR.JsonSerializerDoesNotSupportComments, paramName);
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_SerializationNotSupported(Type propertyType)
        {
            throw new NotSupportedException(SR.Format(SR.SerializationNotSupportedType, propertyType));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_TypeRequiresAsyncSerialization(Type propertyType)
        {
            throw new NotSupportedException(SR.Format(SR.TypeRequiresAsyncSerialization, propertyType));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_DictionaryKeyTypeNotSupported(Type keyType, JsonConverter converter)
        {
            throw new NotSupportedException(SR.Format(SR.DictionaryKeyTypeNotSupported, keyType, converter.GetType()));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_DeserializeUnableToConvertValue(Type propertyType)
        {
            throw new JsonException(SR.Format(SR.DeserializeUnableToConvertValue, propertyType)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowInvalidCastException_DeserializeUnableToAssignValue(Type typeOfValue, Type declaredType)
        {
            throw new InvalidCastException(SR.Format(SR.DeserializeUnableToAssignValue, typeOfValue, declaredType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_DeserializeUnableToAssignNull(Type declaredType)
        {
            throw new InvalidOperationException(SR.Format(SR.DeserializeUnableToAssignNull, declaredType));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_PropertyGetterDisallowNull(string propertyName, Type declaringType)
        {
            throw new JsonException(SR.Format(SR.PropertyGetterDisallowNull, propertyName, declaringType)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowJsonException_PropertySetterDisallowNull(string propertyName, Type declaringType)
        {
            throw new JsonException(SR.Format(SR.PropertySetterDisallowNull, propertyName, declaringType)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowJsonException_ConstructorParameterDisallowNull(string parameterName, Type declaringType)
        {
            throw new JsonException(SR.Format(SR.ConstructorParameterDisallowNull, parameterName, declaringType)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ObjectCreationHandlingPopulateNotSupportedByConverter(JsonPropertyInfo propertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPopulateNotSupportedByConverter, propertyInfo.Name, propertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyMustHaveAGetter(JsonPropertyInfo propertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyMustHaveAGetter, propertyInfo.Name, propertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyValueTypeMustHaveASetter(JsonPropertyInfo propertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyValueTypeMustHaveASetter, propertyInfo.Name, propertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization(JsonPropertyInfo propertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyCannotAllowPolymorphicDeserialization, propertyInfo.Name, propertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowReadOnlyMember(JsonPropertyInfo propertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ObjectCreationHandlingPropertyCannotAllowReadOnlyMember, propertyInfo.Name, propertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ObjectCreationHandlingPropertyCannotAllowReferenceHandling()
        {
            throw new InvalidOperationException(SR.ObjectCreationHandlingPropertyCannotAllowReferenceHandling);
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors()
        {
            throw new NotSupportedException(SR.ObjectCreationHandlingPropertyDoesNotSupportParameterizedConstructors);
        }

        [DoesNotReturn]
        public static void ThrowJsonException_SerializationConverterRead(JsonConverter? converter)
        {
            throw new JsonException(SR.Format(SR.SerializationConverterRead, converter)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowJsonException_SerializationConverterWrite(JsonConverter? converter)
        {
            throw new JsonException(SR.Format(SR.SerializationConverterWrite, converter)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowJsonException_SerializerCycleDetected(int maxDepth)
        {
            throw new JsonException(SR.Format(SR.SerializerCycleDetected, maxDepth)) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowJsonException(string? message = null)
        {
            throw new JsonException(message) { AppendPathInformation = true };
        }

        [DoesNotReturn]
        public static void ThrowArgumentException_CannotSerializeInvalidType(string paramName, Type typeToConvert, Type? declaringType, string? propertyName)
        {
            if (declaringType == null)
            {
                Debug.Assert(propertyName == null);
                throw new ArgumentException(SR.Format(SR.CannotSerializeInvalidType, typeToConvert), paramName);
            }

            Debug.Assert(propertyName != null);
            throw new ArgumentException(SR.Format(SR.CannotSerializeInvalidMember, typeToConvert, propertyName, declaringType), paramName);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_CannotSerializeInvalidType(Type typeToConvert, Type? declaringType, MemberInfo? memberInfo)
        {
            if (declaringType == null)
            {
                Debug.Assert(memberInfo == null);
                throw new InvalidOperationException(SR.Format(SR.CannotSerializeInvalidType, typeToConvert));
            }

            Debug.Assert(memberInfo != null);
            throw new InvalidOperationException(SR.Format(SR.CannotSerializeInvalidMember, typeToConvert, memberInfo.Name, declaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationConverterNotCompatible(Type converterType, Type type)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializationConverterNotCompatible, converterType, type));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ResolverTypeNotCompatible(Type requestedType, Type actualType)
        {
            throw new InvalidOperationException(SR.Format(SR.ResolverTypeNotCompatible, actualType, requestedType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ResolverTypeInfoOptionsNotCompatible()
        {
            throw new InvalidOperationException(SR.ResolverTypeInfoOptionsNotCompatible);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonSerializerOptionsNoTypeInfoResolverSpecified()
        {
            throw new InvalidOperationException(SR.JsonSerializerOptionsNoTypeInfoResolverSpecified);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonSerializerIsReflectionDisabled()
        {
            throw new InvalidOperationException(SR.JsonSerializerIsReflectionDisabled);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationConverterOnAttributeInvalid(Type classType, MemberInfo? memberInfo)
        {
            string location = classType.ToString();
            if (memberInfo != null)
            {
                location += $".{memberInfo.Name}";
            }

            throw new InvalidOperationException(SR.Format(SR.SerializationConverterOnAttributeInvalid, location));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationConverterOnAttributeNotCompatible(Type classTypeAttributeIsOn, MemberInfo? memberInfo, Type typeToConvert)
        {
            string location = classTypeAttributeIsOn.ToString();

            if (memberInfo != null)
            {
                location += $".{memberInfo.Name}";
            }

            throw new InvalidOperationException(SR.Format(SR.SerializationConverterOnAttributeNotCompatible, location, typeToConvert));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializerOptionsReadOnly(JsonSerializerContext? context)
        {
            string message = context == null
                ? SR.SerializerOptionsReadOnly
                : SR.SerializerContextOptionsReadOnly;

            throw new InvalidOperationException(message);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_DefaultTypeInfoResolverImmutable()
        {
            throw new InvalidOperationException(SR.DefaultTypeInfoResolverImmutable);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_TypeInfoResolverChainImmutable()
        {
            throw new InvalidOperationException(SR.TypeInfoResolverChainImmutable);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_TypeInfoImmutable()
        {
            throw new InvalidOperationException(SR.TypeInfoImmutable);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_InvalidChainedResolver()
        {
            throw new InvalidOperationException(SR.SerializerOptions_InvalidChainedResolver);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializerPropertyNameConflict(Type type, string propertyName)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializerPropertyNameConflict, type, propertyName));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializerPropertyNameNull(JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializerPropertyNameNull, jsonPropertyInfo.DeclaringType, jsonPropertyInfo.MemberName));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonPropertyRequiredAndNotDeserializable(JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.JsonPropertyRequiredAndNotDeserializable, jsonPropertyInfo.Name, jsonPropertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonPropertyRequiredAndExtensionData(JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.JsonPropertyRequiredAndExtensionData, jsonPropertyInfo.Name, jsonPropertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_JsonRequiredPropertyMissing(JsonTypeInfo parent, BitArray assignedOrNotRequiredPropertiesSet)
        {
            StringBuilder listOfMissingPropertiesBuilder = new();
            bool first = true;

            // Soft cut-off length - once message becomes longer than that we won't be adding more elements
            const int CutOffLength = 60;

            foreach (JsonPropertyInfo property in parent.PropertyCache)
            {
                if (assignedOrNotRequiredPropertiesSet[property.PropertyIndex])
                {
                    continue;
                }

                if (!first)
                {
                    listOfMissingPropertiesBuilder.Append(CultureInfo.CurrentUICulture.TextInfo.ListSeparator);
                    listOfMissingPropertiesBuilder.Append(' ');
                }

                listOfMissingPropertiesBuilder.Append('\'');
                listOfMissingPropertiesBuilder.Append(property.Name);
                listOfMissingPropertiesBuilder.Append('\'');
                first = false;

                if (listOfMissingPropertiesBuilder.Length >= CutOffLength)
                {
                    break;
                }
            }

            throw new JsonException(SR.Format(SR.JsonRequiredPropertiesMissing, parent.Type, listOfMissingPropertiesBuilder.ToString()));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_DuplicatePropertyNotAllowed(JsonPropertyInfo property)
        {
            throw new JsonException(SR.Format(SR.DuplicatePropertiesNotAllowed_JsonPropertyInfo, property.Name, property.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_DuplicatePropertyNotAllowed()
        {
            throw new JsonException(SR.Format(SR.DuplicatePropertiesNotAllowed));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_DuplicatePropertyNotAllowed(string name)
        {
            throw new JsonException(SR.Format(SR.DuplicatePropertiesNotAllowed_NameSpan, Truncate(name)));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_DuplicatePropertyNotAllowed(ReadOnlySpan<byte> nameBytes)
        {
            string name = JsonHelpers.Utf8GetString(nameBytes);
            throw new JsonException(SR.Format(SR.DuplicatePropertiesNotAllowed_NameSpan, Truncate(name)));
        }

        private static string Truncate(ReadOnlySpan<char> str)
        {
            const int MaxLength = 15;

            if (str.Length <= MaxLength)
            {
                return str.ToString();
            }

            Span<char> builder = stackalloc char[MaxLength + 3];
            str.Slice(0, MaxLength).CopyTo(builder);
            builder[MaxLength] = builder[MaxLength + 1] = builder[MaxLength + 2] = '.';
            return builder.ToString();
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_NamingPolicyReturnNull(JsonNamingPolicy namingPolicy)
        {
            throw new InvalidOperationException(SR.Format(SR.NamingPolicyReturnNull, namingPolicy));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializerConverterFactoryReturnsNull(Type converterType)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializerConverterFactoryReturnsNull, converterType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializerConverterFactoryReturnsJsonConverterFactorty(Type converterType)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializerConverterFactoryReturnsJsonConverterFactory, converterType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_MultiplePropertiesBindToConstructorParameters(
            Type parentType,
            string parameterName,
            string firstMatchName,
            string secondMatchName)
        {
            throw new InvalidOperationException(
                SR.Format(
                    SR.MultipleMembersBindWithConstructorParameter,
                    firstMatchName,
                    secondMatchName,
                    parentType,
                    parameterName));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ConstructorParameterIncompleteBinding(Type parentType)
        {
            throw new InvalidOperationException(SR.Format(SR.ConstructorParamIncompleteBinding, parentType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ExtensionDataCannotBindToCtorParam(string propertyName, JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ExtensionDataCannotBindToCtorParam, propertyName, jsonPropertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonIncludeOnInaccessibleProperty(string memberName, Type declaringType)
        {
            throw new InvalidOperationException(SR.Format(SR.JsonIncludeOnInaccessibleProperty, memberName, declaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_IgnoreConditionOnValueTypeInvalid(string clrPropertyName, Type propertyDeclaringType)
        {
            throw new InvalidOperationException(SR.Format(SR.IgnoreConditionOnValueTypeInvalid, clrPropertyName, propertyDeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_NumberHandlingOnPropertyInvalid(JsonPropertyInfo jsonPropertyInfo)
        {
            Debug.Assert(!jsonPropertyInfo.IsForTypeInfo);
            throw new InvalidOperationException(SR.Format(SR.NumberHandlingOnPropertyInvalid, jsonPropertyInfo.MemberName, jsonPropertyInfo.DeclaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ConverterCanConvertMultipleTypes(Type runtimePropertyType, JsonConverter jsonConverter)
        {
            throw new InvalidOperationException(SR.Format(SR.ConverterCanConvertMultipleTypes, jsonConverter.GetType(), jsonConverter.Type, runtimePropertyType));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(
            ReadOnlySpan<byte> propertyName,
            ref Utf8JsonReader reader,
            scoped ref ReadStack state)
        {
            JsonTypeInfo jsonTypeInfo = state.GetTopJsonTypeInfoWithParameterizedConstructor();
            state.Current.JsonPropertyName = propertyName.ToArray();

            NotSupportedException ex = new NotSupportedException(
                SR.Format(SR.ObjectWithParameterizedCtorRefMetadataNotSupported, jsonTypeInfo.Type));
            ThrowNotSupportedException(ref state, reader, ex);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonTypeInfoOperationNotPossibleForKind(JsonTypeInfoKind kind)
        {
            throw new InvalidOperationException(SR.Format(SR.InvalidJsonTypeInfoOperationForKind, kind));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonTypeInfoOnDeserializingCallbacksNotSupported(Type type)
        {
            throw new InvalidOperationException(SR.Format(SR.OnDeserializingCallbacksNotSupported, type));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_CreateObjectConverterNotCompatible(Type type)
        {
            throw new InvalidOperationException(SR.Format(SR.CreateObjectConverterNotCompatible, type));
        }

        [DoesNotReturn]
        public static void ReThrowWithPath(scoped ref ReadStack state, JsonReaderException ex)
        {
            Debug.Assert(ex.Path == null);

            string path = state.JsonPath();
            string message = ex.Message;

            // Insert the "Path" portion before "LineNumber" and "BytePositionInLine".
#if NET
            int iPos = message.AsSpan().LastIndexOf(" LineNumber: ");
#else
            int iPos = message.LastIndexOf(" LineNumber: ", StringComparison.Ordinal);
#endif
            if (iPos >= 0)
            {
                message = $"{message.Substring(0, iPos)} Path: {path} |{message.Substring(iPos)}";
            }
            else
            {
                message += $" Path: {path}.";
            }

            throw new JsonException(message, path, ex.LineNumber, ex.BytePositionInLine, ex);
        }

        [DoesNotReturn]
        public static void ReThrowWithPath(scoped ref ReadStack state, in Utf8JsonReader reader, Exception ex)
        {
            JsonException jsonException = new JsonException(null, ex);
            AddJsonExceptionInformation(ref state, reader, jsonException);
            throw jsonException;
        }

        public static void AddJsonExceptionInformation(scoped ref ReadStack state, in Utf8JsonReader reader, JsonException ex)
        {
            Debug.Assert(ex.Path is null); // do not overwrite existing path information

            long lineNumber = reader.CurrentState._lineNumber;
            ex.LineNumber = lineNumber;

            long bytePositionInLine = reader.CurrentState._bytePositionInLine;
            ex.BytePositionInLine = bytePositionInLine;

            string path = state.JsonPath();
            ex.Path = path;

            string? message = ex._message;

            if (string.IsNullOrEmpty(message))
            {
                // Use a default message.
                Type propertyType = state.Current.JsonPropertyInfo?.PropertyType ?? state.Current.JsonTypeInfo.Type;
                message = SR.Format(SR.DeserializeUnableToConvertValue, propertyType);
                ex.AppendPathInformation = true;
            }

            if (ex.AppendPathInformation)
            {
                message += $" Path: {path} | LineNumber: {lineNumber} | BytePositionInLine: {bytePositionInLine}.";
                ex.SetMessage(message);
            }
        }

        [DoesNotReturn]
        public static void ReThrowWithPath(ref WriteStack state, Exception ex)
        {
            JsonException jsonException = new JsonException(null, ex);
            AddJsonExceptionInformation(ref state, jsonException);
            throw jsonException;
        }

        public static void AddJsonExceptionInformation(ref WriteStack state, JsonException ex)
        {
            Debug.Assert(ex.Path is null); // do not overwrite existing path information

            string path = state.PropertyPath();
            ex.Path = path;

            string? message = ex._message;
            if (string.IsNullOrEmpty(message))
            {
                // Use a default message.
                message = SR.Format(SR.SerializeUnableToSerialize);
                ex.AppendPathInformation = true;
            }

            if (ex.AppendPathInformation)
            {
                message += $" Path: {path}.";
                ex.SetMessage(message);
            }
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationDuplicateAttribute(Type attribute, MemberInfo memberInfo)
        {
            string location = memberInfo is Type type ? type.ToString() : $"{memberInfo.DeclaringType}.{memberInfo.Name}";
            throw new InvalidOperationException(SR.Format(SR.SerializationDuplicateAttribute, attribute, location));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationDuplicateTypeAttribute(Type classType, Type attribute)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializationDuplicateTypeAttribute, classType, attribute));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationDuplicateTypeAttribute<TAttribute>(Type classType)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializationDuplicateTypeAttribute, classType, typeof(TAttribute)));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_ExtensionDataConflictsWithUnmappedMemberHandling(Type classType, JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.ExtensionDataConflictsWithUnmappedMemberHandling, classType, jsonPropertyInfo.MemberName));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_SerializationDataExtensionPropertyInvalid(JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.SerializationDataExtensionPropertyInvalid, jsonPropertyInfo.PropertyType, jsonPropertyInfo.MemberName));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_PropertyTypeNotNullable(JsonPropertyInfo jsonPropertyInfo)
        {
            throw new InvalidOperationException(SR.Format(SR.PropertyTypeNotNullable, jsonPropertyInfo.PropertyType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty()
        {
            throw new InvalidOperationException(SR.NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty);
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException(scoped ref ReadStack state, in Utf8JsonReader reader, Exception innerException)
        {
            string message = innerException.Message;

            // The caller should check to ensure path is not already set.
            Debug.Assert(!message.Contains(" Path: "));

            // Obtain the type to show in the message.
            Type propertyType = state.Current.JsonPropertyInfo?.PropertyType ?? state.Current.JsonTypeInfo.Type;

            if (!message.Contains(propertyType.ToString()))
            {
                if (message.Length > 0)
                {
                    message += " ";
                }

                message += SR.Format(SR.SerializationNotSupportedParentType, propertyType);
            }

            long lineNumber = reader.CurrentState._lineNumber;
            long bytePositionInLine = reader.CurrentState._bytePositionInLine;
            message += $" Path: {state.JsonPath()} | LineNumber: {lineNumber} | BytePositionInLine: {bytePositionInLine}.";

            throw new NotSupportedException(message, innerException);
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException(ref WriteStack state, Exception innerException)
        {
            string message = innerException.Message;

            // The caller should check to ensure path is not already set.
            Debug.Assert(!message.Contains(" Path: "));

            // Obtain the type to show in the message.
            Type propertyType = state.Current.JsonPropertyInfo?.PropertyType ?? state.Current.JsonTypeInfo.Type;

            if (!message.Contains(propertyType.ToString()))
            {
                if (message.Length > 0)
                {
                    message += " ";
                }

                message += SR.Format(SR.SerializationNotSupportedParentType, propertyType);
            }

            message += $" Path: {state.PropertyPath()}.";

            throw new NotSupportedException(message, innerException);
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_DeserializeNoConstructor(JsonTypeInfo typeInfo, ref Utf8JsonReader reader, scoped ref ReadStack state)
        {
            Type type = typeInfo.Type;
            string message;

            if (type.IsInterface || type.IsAbstract)
            {
                if (typeInfo.PolymorphicTypeResolver?.UsesTypeDiscriminators is true)
                {
                    message = SR.Format(SR.DeserializationMustSpecifyTypeDiscriminator, type);
                }
                else if (typeInfo.Kind is JsonTypeInfoKind.Enumerable or JsonTypeInfoKind.Dictionary)
                {
                    message = SR.Format(SR.CannotPopulateCollection, type);
                }
                else
                {
                    message = SR.Format(SR.DeserializeInterfaceOrAbstractType, type);
                }
            }
            else
            {
                message = SR.Format(SR.DeserializeNoConstructor, nameof(JsonConstructorAttribute), type);
            }

            ThrowNotSupportedException(ref state, reader, new NotSupportedException(message));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_CannotPopulateCollection(Type type, ref Utf8JsonReader reader, scoped ref ReadStack state)
        {
            ThrowNotSupportedException(ref state, reader, new NotSupportedException(SR.Format(SR.CannotPopulateCollection, type)));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataValuesInvalidToken(JsonTokenType tokenType)
        {
            ThrowJsonException(SR.Format(SR.MetadataInvalidTokenAfterValues, tokenType));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataReferenceNotFound(string id)
        {
            ThrowJsonException(SR.Format(SR.MetadataReferenceNotFound, id));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataValueWasNotString(JsonTokenType tokenType)
        {
            ThrowJsonException(SR.Format(SR.MetadataValueWasNotString, tokenType));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataValueWasNotString(JsonValueKind valueKind)
        {
            ThrowJsonException(SR.Format(SR.MetadataValueWasNotString, valueKind));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties(ReadOnlySpan<byte> propertyName, scoped ref ReadStack state)
        {
            state.Current.JsonPropertyName = propertyName.ToArray();
            ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties();
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataUnexpectedProperty(ReadOnlySpan<byte> propertyName, scoped ref ReadStack state)
        {
            state.Current.JsonPropertyName = propertyName.ToArray();
            ThrowJsonException(SR.Format(SR.MetadataUnexpectedProperty));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_UnmappedJsonProperty(Type type, string unmappedPropertyName)
        {
            throw new JsonException(SR.Format(SR.UnmappedJsonProperty, unmappedPropertyName, type));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties()
        {
            ThrowJsonException(SR.MetadataReferenceCannotContainOtherProperties);
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataIdCannotBeCombinedWithRef(ReadOnlySpan<byte> propertyName, scoped ref ReadStack state)
        {
            state.Current.JsonPropertyName = propertyName.ToArray();
            ThrowJsonException(SR.MetadataIdCannotBeCombinedWithRef);
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataStandaloneValuesProperty(scoped ref ReadStack state, ReadOnlySpan<byte> propertyName)
        {
            state.Current.JsonPropertyName = propertyName.ToArray();
            ThrowJsonException(SR.MetadataStandaloneValuesProperty);
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataInvalidPropertyWithLeadingDollarSign(ReadOnlySpan<byte> propertyName, scoped ref ReadStack state, in Utf8JsonReader reader)
        {
            // Set PropertyInfo or KeyName to write down the conflicting property name in JsonException.Path
            if (state.Current.IsProcessingDictionary())
            {
                state.Current.JsonPropertyNameAsString = reader.GetString();
            }
            else
            {
                state.Current.JsonPropertyName = propertyName.ToArray();
            }

            ThrowJsonException(SR.MetadataInvalidPropertyWithLeadingDollarSign);
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataDuplicateIdFound(string id)
        {
            ThrowJsonException(SR.Format(SR.MetadataDuplicateIdFound, id));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_DuplicateMetadataProperty(ReadOnlySpan<byte> utf8PropertyName)
        {
            ThrowJsonException(SR.Format(SR.DuplicateMetadataProperty, JsonHelpers.Utf8GetString(utf8PropertyName)));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataInvalidReferenceToValueType(Type propertyType)
        {
            ThrowJsonException(SR.Format(SR.MetadataInvalidReferenceToValueType, propertyType));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataInvalidPropertyInArrayMetadata(scoped ref ReadStack state, Type propertyType, in Utf8JsonReader reader)
        {
            state.Current.JsonPropertyName = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan.ToArray();
            string propertyNameAsString = reader.GetString()!;

            ThrowJsonException(SR.Format(SR.MetadataPreservedArrayFailed,
                SR.Format(SR.MetadataInvalidPropertyInArrayMetadata, propertyNameAsString),
                SR.Format(SR.DeserializeUnableToConvertValue, propertyType)));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataPreservedArrayValuesNotFound(scoped ref ReadStack state, Type propertyType)
        {
            // Missing $values, JSON path should point to the property's object.
            state.Current.JsonPropertyName = null;

            ThrowJsonException(SR.Format(SR.MetadataPreservedArrayFailed,
                SR.MetadataStandaloneValuesProperty,
                SR.Format(SR.DeserializeUnableToConvertValue, propertyType)));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_MetadataCannotParsePreservedObjectIntoImmutable(Type propertyType)
        {
            ThrowJsonException(SR.Format(SR.MetadataCannotParsePreservedObjectToImmutable, propertyType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_MetadataReferenceOfTypeCannotBeAssignedToType(string referenceId, Type currentType, Type typeToConvert)
        {
            throw new InvalidOperationException(SR.Format(SR.MetadataReferenceOfTypeCannotBeAssignedToType, referenceId, currentType, typeToConvert));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonPropertyInfoIsBoundToDifferentJsonTypeInfo(JsonPropertyInfo propertyInfo)
        {
            Debug.Assert(propertyInfo.DeclaringTypeInfo != null, "We should not throw this exception when ParentTypeInfo is null");
            throw new InvalidOperationException(SR.Format(SR.JsonPropertyInfoBoundToDifferentParent, propertyInfo.Name, propertyInfo.DeclaringTypeInfo.Type.FullName));
        }

        [DoesNotReturn]
        internal static void ThrowUnexpectedMetadataException(
            ReadOnlySpan<byte> propertyName,
            ref Utf8JsonReader reader,
            scoped ref ReadStack state)
        {

            MetadataPropertyName name = JsonSerializer.GetMetadataPropertyName(propertyName, state.Current.BaseJsonTypeInfo.PolymorphicTypeResolver);
            if (name != 0)
            {
                ThrowJsonException_MetadataUnexpectedProperty(propertyName, ref state);
            }
            else
            {
                ThrowJsonException_MetadataInvalidPropertyWithLeadingDollarSign(propertyName, ref state, reader);
            }
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_NoMetadataForType(Type type, IJsonTypeInfoResolver? resolver)
        {
            throw new NotSupportedException(SR.Format(SR.NoMetadataForType, type, resolver?.ToString() ?? "<null>"));
        }

        public static NotSupportedException GetNotSupportedException_AmbiguousMetadataForType(Type type, Type match1, Type match2)
        {
            return new NotSupportedException(SR.Format(SR.AmbiguousMetadataForType, type, match1, match2));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_ConstructorContainsNullParameterNames(Type declaringType)
        {
            throw new NotSupportedException(SR.Format(SR.ConstructorContainsNullParameterNames, declaringType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_NoMetadataForType(Type type, IJsonTypeInfoResolver? resolver)
        {
            throw new InvalidOperationException(SR.Format(SR.NoMetadataForType, type, resolver?.ToString() ?? "<null>"));
        }

        public static Exception GetInvalidOperationException_NoMetadataForTypeProperties(IJsonTypeInfoResolver? resolver, Type type)
        {
            return new InvalidOperationException(SR.Format(SR.NoMetadataForTypeProperties, resolver?.ToString() ?? "<null>", type));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_NoMetadataForTypeProperties(IJsonTypeInfoResolver? resolver, Type type)
        {
            throw GetInvalidOperationException_NoMetadataForTypeProperties(resolver, type);
        }

        [DoesNotReturn]
        public static void ThrowMissingMemberException_MissingFSharpCoreMember(string missingFsharpCoreMember)
        {
            throw new MissingMemberException(SR.Format(SR.MissingFSharpCoreMember, missingFsharpCoreMember));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_BaseConverterDoesNotSupportMetadata(Type derivedType)
        {
            throw new NotSupportedException(SR.Format(SR.Polymorphism_DerivedConverterDoesNotSupportMetadata, derivedType));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_DerivedConverterDoesNotSupportMetadata(Type derivedType)
        {
            throw new NotSupportedException(SR.Format(SR.Polymorphism_DerivedConverterDoesNotSupportMetadata, derivedType));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_RuntimeTypeNotSupported(Type baseType, Type runtimeType)
        {
            throw new NotSupportedException(SR.Format(SR.Polymorphism_RuntimeTypeNotSupported, runtimeType, baseType));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_RuntimeTypeDiamondAmbiguity(Type baseType, Type runtimeType, Type derivedType1, Type derivedType2)
        {
            throw new NotSupportedException(SR.Format(SR.Polymorphism_RuntimeTypeDiamondAmbiguity, runtimeType, derivedType1, derivedType2, baseType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_TypeDoesNotSupportPolymorphism(Type baseType)
        {
            throw new InvalidOperationException(SR.Format(SR.Polymorphism_TypeDoesNotSupportPolymorphism, baseType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_DerivedTypeNotSupported(Type baseType, Type derivedType)
        {
            throw new InvalidOperationException(SR.Format(SR.Polymorphism_DerivedTypeIsNotSupported, derivedType, baseType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_DerivedTypeIsAlreadySpecified(Type baseType, Type derivedType)
        {
            throw new InvalidOperationException(SR.Format(SR.Polymorphism_DerivedTypeIsAlreadySpecified, baseType, derivedType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_TypeDicriminatorIdIsAlreadySpecified(Type baseType, object typeDiscriminator)
        {
            throw new InvalidOperationException(SR.Format(SR.Polymorphism_TypeDicriminatorIdIsAlreadySpecified, baseType, typeDiscriminator));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_InvalidCustomTypeDiscriminatorPropertyName()
        {
            throw new InvalidOperationException(SR.Polymorphism_InvalidCustomTypeDiscriminatorPropertyName);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_PropertyConflictsWithMetadataPropertyName(Type type, string propertyName)
        {
            throw new InvalidOperationException(SR.Format(SR.Polymorphism_PropertyConflictsWithMetadataPropertyName, type, propertyName));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_PolymorphicTypeConfigurationDoesNotSpecifyDerivedTypes(Type baseType)
        {
            throw new InvalidOperationException(SR.Format(SR.Polymorphism_ConfigurationDoesNotSpecifyDerivedTypes, baseType));
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_UnsupportedEnumIdentifier(Type enumType, string? enumName)
        {
            throw new InvalidOperationException(SR.Format(SR.UnsupportedEnumIdentifier, enumType.Name, enumName));
        }

        [DoesNotReturn]
        public static void ThrowJsonException_UnrecognizedTypeDiscriminator(object typeDiscriminator)
        {
            ThrowJsonException(SR.Format(SR.Polymorphism_UnrecognizedTypeDiscriminator, typeDiscriminator));
        }

        [DoesNotReturn]
        public static void ThrowArgumentException_JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo(string parameterName)
        {
            throw new ArgumentException(SR.JsonPolymorphismOptionsAssociatedWithDifferentJsonTypeInfo, paramName: parameterName);
        }

        [DoesNotReturn]
        public static void ThrowOperationCanceledException_PipeWriteCanceled()
        {
            throw new OperationCanceledException(SR.PipeWriterCanceled);
        }

        [DoesNotReturn]
        public static void ThrowOperationCanceledException_PipeReadCanceled()
        {
            throw new OperationCanceledException(SR.PipeReaderCanceled);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_PipeWriterDoesNotImplementUnflushedBytes(PipeWriter pipeWriter)
        {
            throw new InvalidOperationException(SR.Format(SR.PipeWriter_DoesNotImplementUnflushedBytes, pipeWriter.GetType().Name));
        }

        [DoesNotReturn]
        public static void ThrowNotSupportedException_JsonSchemaExporterDoesNotSupportReferenceHandlerPreserve()
        {
            throw new NotSupportedException(SR.JsonSchemaExporter_ReferenceHandlerPreserve_NotSupported);
        }

        [DoesNotReturn]
        public static void ThrowInvalidOperationException_JsonSchemaExporterDepthTooLarge()
        {
            throw new InvalidOperationException(SR.JsonSchemaExporter_DepthTooLarge);
        }
    }
}
