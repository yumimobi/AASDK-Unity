#import "AAObjectCache.h"

@implementation AAObjectCache

+ (instancetype)sharedInstance {
    static AAObjectCache *sharedInstance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (id)init {
    self = [super init];
    if(self){
        _references = [[NSMutableDictionary alloc] init];
    }
    return self;
}

@end

@implementation NSObject (AAOwnershipAdditions)
- (NSString *)aa_referenceKey {
    return [NSString stringWithFormat:@"%p", (void *)self];
}

@end
