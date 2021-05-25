#import <Foundation/Foundation.h>

@interface AAObjectCache : NSObject

+ (instancetype)sharedInstance;

@property(nonatomic, strong) NSMutableDictionary *references;

@end

@interface NSObject (AAOwnershipAdditions)

- (NSString *)aa_referenceKey;

@end
