//
//  BidonUtilities.m
//  Bidon Unity Plugin
//
//  Created by Dmitrii Feshchenko on 02/01/2025.
//

NSString* CreateNSString(const char* cString) {
    return cString ? [NSString stringWithUTF8String: cString] : nil;
}

char* CreateCString(NSString* string) {
    return string ? strdup([string UTF8String]) : NULL;
}
