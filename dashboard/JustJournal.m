#import "JustJournal.h"

@implementation JustJournal

+ (void) login:(NSString *)user  Password:(NSString *) pass {
    NSLog(@"Start Login.");
    NSLog (@"%@", user);
    
    NSString *post = @"key1=val1&key2=val2";
    NSData *postData = [post dataUsingEncoding:NSASCIIStringEncoding allowLossyConversion:YES];
    
    NSString *postLength = [NSString stringWithFormat:@"%d", [postData length]];
    
    NSMutableURLRequest *request = [[[NSMutableURLRequest alloc] init] autorelease];
    [request setURL:[NSURL URLWithString:@"http://www.justjournal.com/updateJournal"]];
    [request setHTTPMethod:@"POST"];
    [request setValue:postLength forHTTPHeaderField:@"Content-Length"];
    [request setValue:@"application/x-www-form-urlencoded" forHTTPHeaderField:@"Content-Type"];
    [request setHTTPBody:postData];
   
    
}
@end
