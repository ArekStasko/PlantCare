interface IClient {
    /**
     * @param body (optional)
     * @return OK
     */
    humidityMeasurements(body?: AddHumidityMeasurementCommand | undefined): Promise<boolean>;
    /**
     * @param id (optional)
     * @param fromDate (optional)
     * @param toDate (optional)
     * @return OK
     */
    humidityMeasurementsAll(id?: number | undefined, fromDate?: Date | undefined, toDate?: Date | undefined): Promise<IHumidityMeasurement[]>;
    /**
     * @param body (optional)
     * @return OK
     */
    status(body?: SetModuleStatusCommand | undefined): Promise<boolean>;
    /**
     * @param body (optional)
     * @return OK
     */
    modules(body?: CreateModuleCommand | undefined): Promise<boolean>;
    /**
     * @return OK
     */
    modulesAll(): Promise<IPlant[]>;
    /**
     * @param body (optional)
     * @return OK
     */
    placesPOST(body?: CreatePlaceCommand | undefined): Promise<boolean>;
    /**
     * @param id (optional)
     * @return OK
     */
    placesDELETE(id?: number | undefined): Promise<boolean>;
    /**
     * @param body (optional)
     * @return OK
     */
    placesPUT(body?: UpdatePlaceCommand | undefined): Promise<boolean>;
    /**
     * @return OK
     */
    placesAll(): Promise<GetPlacesResponse[]>;
    /**
     * @param body (optional)
     * @return OK
     */
    plantsPOST(body?: CreatePlantCommand | undefined): Promise<boolean>;
    /**
     * @param id (optional)
     * @return OK
     */
    plantsDELETE(id?: number | undefined): Promise<boolean>;
    /**
     * @param body (optional)
     * @return OK
     */
    plantsPUT(body?: UpdatePlantCommand | undefined): Promise<boolean>;
    /**
     * @return OK
     */
    plantsAll(): Promise<GetPlantResponse[]>;
    /**
     * @return OK
     */
    anonymous(id: number): Promise<GetPlantResponse>;
}
declare class Client implements IClient {
    private http;
    private baseUrl;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined;
    constructor(baseUrl?: string, http?: {
        fetch(url: RequestInfo, init?: RequestInit): Promise<Response>;
    });
    /**
     * @param body (optional)
     * @return OK
     */
    humidityMeasurements(body?: AddHumidityMeasurementCommand | undefined): Promise<boolean>;
    protected processHumidityMeasurements(response: Response): Promise<boolean>;
    /**
     * @param id (optional)
     * @param fromDate (optional)
     * @param toDate (optional)
     * @return OK
     */
    humidityMeasurementsAll(id?: number | undefined, fromDate?: Date | undefined, toDate?: Date | undefined): Promise<IHumidityMeasurement[]>;
    protected processHumidityMeasurementsAll(response: Response): Promise<IHumidityMeasurement[]>;
    /**
     * @param body (optional)
     * @return OK
     */
    status(body?: SetModuleStatusCommand | undefined): Promise<boolean>;
    protected processStatus(response: Response): Promise<boolean>;
    /**
     * @param body (optional)
     * @return OK
     */
    modules(body?: CreateModuleCommand | undefined): Promise<boolean>;
    protected processModules(response: Response): Promise<boolean>;
    /**
     * @return OK
     */
    modulesAll(): Promise<IPlant[]>;
    protected processModulesAll(response: Response): Promise<IPlant[]>;
    /**
     * @param body (optional)
     * @return OK
     */
    placesPOST(body?: CreatePlaceCommand | undefined): Promise<boolean>;
    protected processPlacesPOST(response: Response): Promise<boolean>;
    /**
     * @param id (optional)
     * @return OK
     */
    placesDELETE(id?: number | undefined): Promise<boolean>;
    protected processPlacesDELETE(response: Response): Promise<boolean>;
    /**
     * @param body (optional)
     * @return OK
     */
    placesPUT(body?: UpdatePlaceCommand | undefined): Promise<boolean>;
    protected processPlacesPUT(response: Response): Promise<boolean>;
    /**
     * @return OK
     */
    placesAll(): Promise<GetPlacesResponse[]>;
    protected processPlacesAll(response: Response): Promise<GetPlacesResponse[]>;
    /**
     * @param body (optional)
     * @return OK
     */
    plantsPOST(body?: CreatePlantCommand | undefined): Promise<boolean>;
    protected processPlantsPOST(response: Response): Promise<boolean>;
    /**
     * @param id (optional)
     * @return OK
     */
    plantsDELETE(id?: number | undefined): Promise<boolean>;
    protected processPlantsDELETE(response: Response): Promise<boolean>;
    /**
     * @param body (optional)
     * @return OK
     */
    plantsPUT(body?: UpdatePlantCommand | undefined): Promise<boolean>;
    protected processPlantsPUT(response: Response): Promise<boolean>;
    /**
     * @return OK
     */
    plantsAll(): Promise<GetPlantResponse[]>;
    protected processPlantsAll(response: Response): Promise<GetPlantResponse[]>;
    /**
     * @return OK
     */
    anonymous(id: number): Promise<GetPlantResponse>;
    protected processAnonymous(response: Response): Promise<GetPlantResponse>;
}
interface AddHumidityMeasurementCommand {
    moduleId?: number;
    humidity?: number;
    measurementDate?: Date;
}
interface Assembly {
    readonly definedTypes?: TypeInfo[] | undefined;
    readonly exportedTypes?: Type[] | undefined;
    readonly codeBase?: string | undefined;
    entryPoint?: MethodInfo;
    readonly fullName?: string | undefined;
    readonly imageRuntimeVersion?: string | undefined;
    readonly isDynamic?: boolean;
    readonly location?: string | undefined;
    readonly reflectionOnly?: boolean;
    readonly isCollectible?: boolean;
    readonly isFullyTrusted?: boolean;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly escapedCodeBase?: string | undefined;
    manifestModule?: Module;
    readonly modules?: Module[] | undefined;
    readonly globalAssemblyCache?: boolean;
    readonly hostContext?: number;
    securityRuleSet?: SecurityRuleSet;
}
declare enum CallingConventions {
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _32 = 32,
    _64 = 64
}
interface ConstructorInfo {
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    attributes?: MethodAttributes;
    methodImplementationFlags?: MethodImplAttributes;
    callingConvention?: CallingConventions;
    readonly isAbstract?: boolean;
    readonly isConstructor?: boolean;
    readonly isFinal?: boolean;
    readonly isHideBySig?: boolean;
    readonly isSpecialName?: boolean;
    readonly isStatic?: boolean;
    readonly isVirtual?: boolean;
    readonly isAssembly?: boolean;
    readonly isFamily?: boolean;
    readonly isFamilyAndAssembly?: boolean;
    readonly isFamilyOrAssembly?: boolean;
    readonly isPrivate?: boolean;
    readonly isPublic?: boolean;
    readonly isConstructedGenericMethod?: boolean;
    readonly isGenericMethod?: boolean;
    readonly isGenericMethodDefinition?: boolean;
    readonly containsGenericParameters?: boolean;
    methodHandle?: RuntimeMethodHandle;
    readonly isSecurityCritical?: boolean;
    readonly isSecuritySafeCritical?: boolean;
    readonly isSecurityTransparent?: boolean;
    memberType?: MemberTypes;
}
interface CreateModuleCommand {
    name?: string | undefined;
    userId?: number;
    address?: string | undefined;
}
interface CreatePlaceCommand {
    name?: string | undefined;
    userId?: number | undefined;
}
interface CreatePlantCommand {
    name?: string | undefined;
    userId?: number;
    description?: string | undefined;
    placeId?: number;
    type?: PlantType;
    moduleId?: number | undefined;
}
interface CustomAttributeData {
    attributeType?: Type;
    constructor?: ConstructorInfo;
    readonly constructorArguments?: CustomAttributeTypedArgument[] | undefined;
    readonly namedArguments?: CustomAttributeNamedArgument[] | undefined;
}
interface CustomAttributeNamedArgument {
    memberInfo?: MemberInfo;
    typedValue?: CustomAttributeTypedArgument;
    readonly memberName?: string | undefined;
    readonly isField?: boolean;
}
interface CustomAttributeTypedArgument {
    argumentType?: Type;
    value?: any | undefined;
}
declare enum EventAttributes {
    _0 = 0,
    _512 = 512,
    _1024 = 1024
}
interface EventInfo {
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    memberType?: MemberTypes;
    attributes?: EventAttributes;
    readonly isSpecialName?: boolean;
    addMethod?: MethodInfo;
    removeMethod?: MethodInfo;
    raiseMethod?: MethodInfo;
    readonly isMulticast?: boolean;
    eventHandlerType?: Type;
}
interface Exception {
    targetSite?: MethodBase;
    readonly message?: string | undefined;
    readonly data?: {
        [key: string]: any;
    } | undefined;
    innerException?: Exception;
    helpLink?: string | undefined;
    source?: string | undefined;
    hResult?: number;
    readonly stackTrace?: string | undefined;
}
declare enum FieldAttributes {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _5 = 5,
    _6 = 6,
    _7 = 7,
    _16 = 16,
    _32 = 32,
    _64 = 64,
    _128 = 128,
    _256 = 256,
    _512 = 512,
    _1024 = 1024,
    _4096 = 4096,
    _8192 = 8192,
    _32768 = 32768,
    _38144 = 38144
}
interface FieldInfo {
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    memberType?: MemberTypes;
    attributes?: FieldAttributes;
    fieldType?: Type;
    readonly isInitOnly?: boolean;
    readonly isLiteral?: boolean;
    readonly isNotSerialized?: boolean;
    readonly isPinvokeImpl?: boolean;
    readonly isSpecialName?: boolean;
    readonly isStatic?: boolean;
    readonly isAssembly?: boolean;
    readonly isFamily?: boolean;
    readonly isFamilyAndAssembly?: boolean;
    readonly isFamilyOrAssembly?: boolean;
    readonly isPrivate?: boolean;
    readonly isPublic?: boolean;
    readonly isSecurityCritical?: boolean;
    readonly isSecuritySafeCritical?: boolean;
    readonly isSecurityTransparent?: boolean;
    fieldHandle?: RuntimeFieldHandle;
}
declare enum GenericParameterAttributes {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _8 = 8,
    _16 = 16,
    _28 = 28
}
interface GetPlacesResponse {
    id?: number;
    name?: string | undefined;
}
interface GetPlantResponse {
    id?: number;
    placeId?: number;
    moduleId?: number;
    name?: string | undefined;
    description?: string | undefined;
    type?: PlantType;
}
interface ICustomAttributeProvider {
}
interface IHumidityMeasurement {
    id?: number;
    moduleId?: number;
    humidity?: number;
    measurementDate?: Date;
}
interface IPlant {
    userId?: number;
    id?: number;
    placeId?: number;
    moduleId?: number;
    name?: string | undefined;
    description?: string | undefined;
    type?: PlantType;
}
interface IntPtr {
}
declare enum LayoutKind {
    _0 = 0,
    _2 = 2,
    _3 = 3
}
interface MemberInfo {
    memberType?: MemberTypes;
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
}
declare enum MemberTypes {
    _1 = 1,
    _2 = 2,
    _4 = 4,
    _8 = 8,
    _16 = 16,
    _32 = 32,
    _64 = 64,
    _128 = 128,
    _191 = 191
}
declare enum MethodAttributes {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _5 = 5,
    _6 = 6,
    _7 = 7,
    _8 = 8,
    _16 = 16,
    _32 = 32,
    _64 = 64,
    _128 = 128,
    _256 = 256,
    _512 = 512,
    _1024 = 1024,
    _2048 = 2048,
    _4096 = 4096,
    _8192 = 8192,
    _16384 = 16384,
    _32768 = 32768,
    _53248 = 53248
}
interface MethodBase {
    memberType?: MemberTypes;
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    attributes?: MethodAttributes;
    methodImplementationFlags?: MethodImplAttributes;
    callingConvention?: CallingConventions;
    readonly isAbstract?: boolean;
    readonly isConstructor?: boolean;
    readonly isFinal?: boolean;
    readonly isHideBySig?: boolean;
    readonly isSpecialName?: boolean;
    readonly isStatic?: boolean;
    readonly isVirtual?: boolean;
    readonly isAssembly?: boolean;
    readonly isFamily?: boolean;
    readonly isFamilyAndAssembly?: boolean;
    readonly isFamilyOrAssembly?: boolean;
    readonly isPrivate?: boolean;
    readonly isPublic?: boolean;
    readonly isConstructedGenericMethod?: boolean;
    readonly isGenericMethod?: boolean;
    readonly isGenericMethodDefinition?: boolean;
    readonly containsGenericParameters?: boolean;
    methodHandle?: RuntimeMethodHandle;
    readonly isSecurityCritical?: boolean;
    readonly isSecuritySafeCritical?: boolean;
    readonly isSecurityTransparent?: boolean;
}
declare enum MethodImplAttributes {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _8 = 8,
    _16 = 16,
    _32 = 32,
    _64 = 64,
    _128 = 128,
    _256 = 256,
    _512 = 512,
    _4096 = 4096,
    _65535 = 65535
}
interface MethodInfo {
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    attributes?: MethodAttributes;
    methodImplementationFlags?: MethodImplAttributes;
    callingConvention?: CallingConventions;
    readonly isAbstract?: boolean;
    readonly isConstructor?: boolean;
    readonly isFinal?: boolean;
    readonly isHideBySig?: boolean;
    readonly isSpecialName?: boolean;
    readonly isStatic?: boolean;
    readonly isVirtual?: boolean;
    readonly isAssembly?: boolean;
    readonly isFamily?: boolean;
    readonly isFamilyAndAssembly?: boolean;
    readonly isFamilyOrAssembly?: boolean;
    readonly isPrivate?: boolean;
    readonly isPublic?: boolean;
    readonly isConstructedGenericMethod?: boolean;
    readonly isGenericMethod?: boolean;
    readonly isGenericMethodDefinition?: boolean;
    readonly containsGenericParameters?: boolean;
    methodHandle?: RuntimeMethodHandle;
    readonly isSecurityCritical?: boolean;
    readonly isSecuritySafeCritical?: boolean;
    readonly isSecurityTransparent?: boolean;
    memberType?: MemberTypes;
    returnParameter?: ParameterInfo;
    returnType?: Type;
    returnTypeCustomAttributes?: ICustomAttributeProvider;
}
interface Module {
    assembly?: Assembly;
    readonly fullyQualifiedName?: string | undefined;
    readonly name?: string | undefined;
    readonly mdStreamVersion?: number;
    readonly moduleVersionId?: string;
    readonly scopeName?: string | undefined;
    moduleHandle?: ModuleHandle;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly metadataToken?: number;
}
interface ModuleHandle {
    readonly mdStreamVersion?: number;
}
declare enum ParameterAttributes {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _4 = 4,
    _8 = 8,
    _16 = 16,
    _4096 = 4096,
    _8192 = 8192,
    _16384 = 16384,
    _32768 = 32768,
    _61440 = 61440
}
interface ParameterInfo {
    attributes?: ParameterAttributes;
    member?: MemberInfo;
    readonly name?: string | undefined;
    parameterType?: Type;
    readonly position?: number;
    readonly isIn?: boolean;
    readonly isLcid?: boolean;
    readonly isOptional?: boolean;
    readonly isOut?: boolean;
    readonly isRetval?: boolean;
    readonly defaultValue?: any | undefined;
    readonly rawDefaultValue?: any | undefined;
    readonly hasDefaultValue?: boolean;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly metadataToken?: number;
}
declare enum PlantType {
    _0 = 0,
    _1 = 1,
    _2 = 2
}
declare enum PropertyAttributes {
    _0 = 0,
    _512 = 512,
    _1024 = 1024,
    _4096 = 4096,
    _8192 = 8192,
    _16384 = 16384,
    _32768 = 32768,
    _62464 = 62464
}
interface PropertyInfo {
    readonly name?: string | undefined;
    declaringType?: Type;
    reflectedType?: Type;
    module?: Module;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    memberType?: MemberTypes;
    propertyType?: Type;
    attributes?: PropertyAttributes;
    readonly isSpecialName?: boolean;
    readonly canRead?: boolean;
    readonly canWrite?: boolean;
    getMethod?: MethodInfo;
    setMethod?: MethodInfo;
}
interface RuntimeFieldHandle {
    value?: IntPtr;
}
interface RuntimeMethodHandle {
    value?: IntPtr;
}
interface RuntimeTypeHandle {
    value?: IntPtr;
}
declare enum SecurityRuleSet {
    _0 = 0,
    _1 = 1,
    _2 = 2
}
interface SetModuleStatusCommand {
    userId?: number | undefined;
    moduleId?: number;
    status?: boolean;
}
interface StructLayoutAttribute {
    readonly typeId?: any | undefined;
    value?: LayoutKind;
}
interface Type {
    readonly name?: string | undefined;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    readonly isInterface?: boolean;
    memberType?: MemberTypes;
    readonly namespace?: string | undefined;
    readonly assemblyQualifiedName?: string | undefined;
    readonly fullName?: string | undefined;
    assembly?: Assembly;
    module?: Module;
    readonly isNested?: boolean;
    declaringType?: Type;
    declaringMethod?: MethodBase;
    reflectedType?: Type;
    underlyingSystemType?: Type;
    readonly isTypeDefinition?: boolean;
    readonly isArray?: boolean;
    readonly isByRef?: boolean;
    readonly isPointer?: boolean;
    readonly isConstructedGenericType?: boolean;
    readonly isGenericParameter?: boolean;
    readonly isGenericTypeParameter?: boolean;
    readonly isGenericMethodParameter?: boolean;
    readonly isGenericType?: boolean;
    readonly isGenericTypeDefinition?: boolean;
    readonly isSZArray?: boolean;
    readonly isVariableBoundArray?: boolean;
    readonly isByRefLike?: boolean;
    readonly isFunctionPointer?: boolean;
    readonly isUnmanagedFunctionPointer?: boolean;
    readonly hasElementType?: boolean;
    readonly genericTypeArguments?: Type[] | undefined;
    readonly genericParameterPosition?: number;
    genericParameterAttributes?: GenericParameterAttributes;
    attributes?: TypeAttributes;
    readonly isAbstract?: boolean;
    readonly isImport?: boolean;
    readonly isSealed?: boolean;
    readonly isSpecialName?: boolean;
    readonly isClass?: boolean;
    readonly isNestedAssembly?: boolean;
    readonly isNestedFamANDAssem?: boolean;
    readonly isNestedFamily?: boolean;
    readonly isNestedFamORAssem?: boolean;
    readonly isNestedPrivate?: boolean;
    readonly isNestedPublic?: boolean;
    readonly isNotPublic?: boolean;
    readonly isPublic?: boolean;
    readonly isAutoLayout?: boolean;
    readonly isExplicitLayout?: boolean;
    readonly isLayoutSequential?: boolean;
    readonly isAnsiClass?: boolean;
    readonly isAutoClass?: boolean;
    readonly isUnicodeClass?: boolean;
    readonly isCOMObject?: boolean;
    readonly isContextful?: boolean;
    readonly isEnum?: boolean;
    readonly isMarshalByRef?: boolean;
    readonly isPrimitive?: boolean;
    readonly isValueType?: boolean;
    readonly isSignatureType?: boolean;
    readonly isSecurityCritical?: boolean;
    readonly isSecuritySafeCritical?: boolean;
    readonly isSecurityTransparent?: boolean;
    structLayoutAttribute?: StructLayoutAttribute;
    typeInitializer?: ConstructorInfo;
    typeHandle?: RuntimeTypeHandle;
    readonly guid?: string;
    baseType?: Type;
    readonly isSerializable?: boolean;
    readonly containsGenericParameters?: boolean;
    readonly isVisible?: boolean;
}
declare enum TypeAttributes {
    _0 = 0,
    _1 = 1,
    _2 = 2,
    _3 = 3,
    _4 = 4,
    _5 = 5,
    _6 = 6,
    _7 = 7,
    _8 = 8,
    _16 = 16,
    _24 = 24,
    _32 = 32,
    _128 = 128,
    _256 = 256,
    _1024 = 1024,
    _2048 = 2048,
    _4096 = 4096,
    _8192 = 8192,
    _16384 = 16384,
    _65536 = 65536,
    _131072 = 131072,
    _196608 = 196608,
    _262144 = 262144,
    _264192 = 264192,
    _1048576 = 1048576,
    _12582912 = 12582912
}
interface TypeInfo {
    readonly name?: string | undefined;
    readonly customAttributes?: CustomAttributeData[] | undefined;
    readonly isCollectible?: boolean;
    readonly metadataToken?: number;
    readonly isInterface?: boolean;
    memberType?: MemberTypes;
    readonly namespace?: string | undefined;
    readonly assemblyQualifiedName?: string | undefined;
    readonly fullName?: string | undefined;
    assembly?: Assembly;
    module?: Module;
    readonly isNested?: boolean;
    declaringType?: Type;
    declaringMethod?: MethodBase;
    reflectedType?: Type;
    underlyingSystemType?: Type;
    readonly isTypeDefinition?: boolean;
    readonly isArray?: boolean;
    readonly isByRef?: boolean;
    readonly isPointer?: boolean;
    readonly isConstructedGenericType?: boolean;
    readonly isGenericParameter?: boolean;
    readonly isGenericTypeParameter?: boolean;
    readonly isGenericMethodParameter?: boolean;
    readonly isGenericType?: boolean;
    readonly isGenericTypeDefinition?: boolean;
    readonly isSZArray?: boolean;
    readonly isVariableBoundArray?: boolean;
    readonly isByRefLike?: boolean;
    readonly isFunctionPointer?: boolean;
    readonly isUnmanagedFunctionPointer?: boolean;
    readonly hasElementType?: boolean;
    readonly genericTypeArguments?: Type[] | undefined;
    readonly genericParameterPosition?: number;
    genericParameterAttributes?: GenericParameterAttributes;
    attributes?: TypeAttributes;
    readonly isAbstract?: boolean;
    readonly isImport?: boolean;
    readonly isSealed?: boolean;
    readonly isSpecialName?: boolean;
    readonly isClass?: boolean;
    readonly isNestedAssembly?: boolean;
    readonly isNestedFamANDAssem?: boolean;
    readonly isNestedFamily?: boolean;
    readonly isNestedFamORAssem?: boolean;
    readonly isNestedPrivate?: boolean;
    readonly isNestedPublic?: boolean;
    readonly isNotPublic?: boolean;
    readonly isPublic?: boolean;
    readonly isAutoLayout?: boolean;
    readonly isExplicitLayout?: boolean;
    readonly isLayoutSequential?: boolean;
    readonly isAnsiClass?: boolean;
    readonly isAutoClass?: boolean;
    readonly isUnicodeClass?: boolean;
    readonly isCOMObject?: boolean;
    readonly isContextful?: boolean;
    readonly isEnum?: boolean;
    readonly isMarshalByRef?: boolean;
    readonly isPrimitive?: boolean;
    readonly isValueType?: boolean;
    readonly isSignatureType?: boolean;
    readonly isSecurityCritical?: boolean;
    readonly isSecuritySafeCritical?: boolean;
    readonly isSecurityTransparent?: boolean;
    structLayoutAttribute?: StructLayoutAttribute;
    typeInitializer?: ConstructorInfo;
    typeHandle?: RuntimeTypeHandle;
    readonly guid?: string;
    baseType?: Type;
    readonly isSerializable?: boolean;
    readonly containsGenericParameters?: boolean;
    readonly isVisible?: boolean;
    readonly genericTypeParameters?: Type[] | undefined;
    readonly declaredConstructors?: ConstructorInfo[] | undefined;
    readonly declaredEvents?: EventInfo[] | undefined;
    readonly declaredFields?: FieldInfo[] | undefined;
    readonly declaredMembers?: MemberInfo[] | undefined;
    readonly declaredMethods?: MethodInfo[] | undefined;
    readonly declaredNestedTypes?: TypeInfo[] | undefined;
    readonly declaredProperties?: PropertyInfo[] | undefined;
    readonly implementedInterfaces?: Type[] | undefined;
}
interface UpdatePlaceCommand {
    id?: number;
    userId?: number | undefined;
    name?: string | undefined;
}
interface UpdatePlantCommand {
    id?: number;
    userId?: number;
    name?: string | undefined;
    description?: string | undefined;
    placeId?: number;
    type?: PlantType;
    moduleId?: number | undefined;
}
declare class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: {
        [key: string]: any;
    };
    result: any;
    constructor(message: string, status: number, response: string, headers: {
        [key: string]: any;
    }, result: any);
    protected isApiException: boolean;
    static isApiException(obj: any): obj is ApiException;
}

export { type AddHumidityMeasurementCommand, ApiException, type Assembly, CallingConventions, Client, type ConstructorInfo, type CreateModuleCommand, type CreatePlaceCommand, type CreatePlantCommand, type CustomAttributeData, type CustomAttributeNamedArgument, type CustomAttributeTypedArgument, EventAttributes, type EventInfo, type Exception, FieldAttributes, type FieldInfo, GenericParameterAttributes, type GetPlacesResponse, type GetPlantResponse, type IClient, type ICustomAttributeProvider, type IHumidityMeasurement, type IPlant, type IntPtr, LayoutKind, type MemberInfo, MemberTypes, MethodAttributes, type MethodBase, MethodImplAttributes, type MethodInfo, type Module, type ModuleHandle, ParameterAttributes, type ParameterInfo, PlantType, PropertyAttributes, type PropertyInfo, type RuntimeFieldHandle, type RuntimeMethodHandle, type RuntimeTypeHandle, SecurityRuleSet, type SetModuleStatusCommand, type StructLayoutAttribute, type Type, TypeAttributes, type TypeInfo, type UpdatePlaceCommand, type UpdatePlantCommand };
