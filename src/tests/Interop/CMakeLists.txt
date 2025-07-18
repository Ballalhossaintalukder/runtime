if(CLR_CMAKE_HOST_WIN32)
    list(APPEND LINK_LIBRARIES_ADDITIONAL
        ole32.lib
        advapi32.lib
        OleAut32.lib
    )
endif(CLR_CMAKE_HOST_WIN32)

list(APPEND LINK_LIBRARIES_ADDITIONAL platformdefines)

# Consumed by native test assets
SET(CLR_INTEROP_TEST_ROOT ${CMAKE_CURRENT_SOURCE_DIR})

include_directories(common)
add_subdirectory(PInvoke/Decimal)
add_subdirectory(PInvoke/ArrayWithOffset)
add_subdirectory(PInvoke/Delegate)
add_subdirectory(PInvoke/Primitives/Int)
add_subdirectory(PInvoke/Primitives/RuntimeHandles)
add_subdirectory(PInvoke/Primitives/Pointer)
add_subdirectory(PInvoke/SetLastError)
add_subdirectory(PInvoke/SizeParamIndex/PInvoke/PassingByOut)
add_subdirectory(PInvoke/SizeParamIndex/PInvoke/PassingByRef)
add_subdirectory(PInvoke/SizeParamIndex/ReversePInvoke/PassingByOut)
add_subdirectory(PInvoke/SizeParamIndex/ReversePInvoke/PassingByRef)
add_subdirectory(PInvoke/Array/MarshalArrayAsField/LPArrayNative)
add_subdirectory(PInvoke/Array/MarshalArrayAsParam/LPArrayNative)
add_subdirectory(PInvoke/Miscellaneous/HandleRef)
add_subdirectory(PInvoke/Miscellaneous/MultipleAssembliesWithSamePInvoke)
add_subdirectory(PInvoke/CriticalHandles)
add_subdirectory(PInvoke/Generics)
add_subdirectory(PInvoke/Int128)
add_subdirectory(PInvoke/AsAny)
add_subdirectory(PInvoke/SafeHandles)
add_subdirectory(PInvoke/Vector2_3_4)
add_subdirectory(UnmanagedCallConv)
add_subdirectory(UnmanagedCallersOnly)
add_subdirectory(PrimitiveMarshalling/Bool)
add_subdirectory(PrimitiveMarshalling/UIntPtr)
add_subdirectory(ArrayMarshalling/BoolArray)
add_subdirectory(PrimitiveMarshalling/EnumMarshalling)
add_subdirectory(FuncPtrAsDelegateParam)
add_subdirectory(SimpleStruct)
add_subdirectory(StructMarshalling/PInvoke)
add_subdirectory(StructMarshalling/ReversePInvoke/MarshalExpStruct)
add_subdirectory(StructMarshalling/ReversePInvoke/MarshalSeqStruct)
add_subdirectory(SuppressGCTransition)
add_subdirectory(BestFitMapping)
add_subdirectory(RefCharArray)
add_subdirectory(StringMarshalling/LPSTR)
add_subdirectory(StringMarshalling/LPTSTR)
add_subdirectory(StringMarshalling/UTF8)
add_subdirectory(StringMarshalling/BSTR)
add_subdirectory(StringMarshalling/AnsiBSTR)
add_subdirectory(StringMarshalling/VBByRefStr)
add_subdirectory(MarshalAPI/FunctionPointer)
add_subdirectory(NativeLibrary/NativeLibraryToLoad)
add_subdirectory(DllImportAttribute/DllImportPath)
add_subdirectory(DllImportAttribute/ExactSpelling)
add_subdirectory(DllImportSearchPaths/NativeLibraryWithDependency)
add_subdirectory(ICustomMarshaler/ConflictingNames)
add_subdirectory(ICustomMarshaler/Primitives)
add_subdirectory(LayoutClass)
add_subdirectory(PInvoke/DateTime)
add_subdirectory(DisabledRuntimeMarshalling)
add_subdirectory(MonoAPI/Native)
add_subdirectory(COM/ComWrappers/MockReferenceTrackerRuntime)
add_subdirectory(COM/ComWrappers/WeakReference)
add_subdirectory(GCBridge)
if(CLR_CMAKE_TARGET_WIN32)
    add_subdirectory(ExecInDefAppDom)
    add_subdirectory(PInvoke/Attributes/LCID)
    add_subdirectory(PInvoke/BestFitMapping/Char)
    add_subdirectory(PInvoke/BestFitMapping/LPStr)
    add_subdirectory(PInvoke/Variant)
    add_subdirectory(PInvoke/Varargs)
    add_subdirectory(PInvoke/NativeCallManagedComVisible)
    add_subdirectory(PInvoke/IEnumerator)
    add_subdirectory(PInvoke/CustomMarshalers)
    add_subdirectory(ArrayMarshalling/SafeArray)
    add_subdirectory(COM/Dynamic/Server)
    add_subdirectory(COM/NativeServer)
    add_subdirectory(COM/NativeClients/Primitives)
    add_subdirectory(COM/NativeClients/Licensing)
    add_subdirectory(COM/NativeClients/DefaultInterfaces)
    add_subdirectory(COM/NativeClients/Dispatch)
    add_subdirectory(COM/NativeClients/Events)
    add_subdirectory(COM/NativeClients/MiscTypes)

    # IJW isn't supported on ARM64
    if(NOT CLR_CMAKE_HOST_ARCH_ARM64)
        add_subdirectory(IJW/ijwhostmock)
        add_subdirectory(IJW/IjwNativeDll)
        add_subdirectory(IJW/IjwNativeCallingManagedDll)
        add_subdirectory(IJW/CopyConstructorMarshaler)
        add_subdirectory(IJW/NativeVarargs)
    endif()
endif(CLR_CMAKE_TARGET_WIN32)

if(CLR_CMAKE_TARGET_APPLE)
    add_subdirectory(ObjectiveC/AutoReleaseTest)
    add_subdirectory(ObjectiveC/ObjectiveCMarshalAPI)
    add_subdirectory(Swift/SwiftAbiStress)
    add_subdirectory(Swift/SwiftCallbackAbiStress)
    add_subdirectory(Swift/SwiftErrorHandling)
    add_subdirectory(Swift/SwiftIndirectResult)
    add_subdirectory(Swift/SwiftInvalidCallConv)
    add_subdirectory(Swift/SwiftRetAbiStress)
    add_subdirectory(Swift/SwiftSelfContext)
    add_subdirectory(Swift/SwiftInlineArray)
endif()
