#import <Foundation/Foundation.h>
#import <mach/mach.h>
 
extern "C" {
    unsigned int getUsedMemorySize() {
        struct task_basic_info basic_info;
        mach_msg_type_number_t t_info_count = TASK_BASIC_INFO_COUNT;
        kern_return_t status;
 
        status = task_info(current_task(), TASK_BASIC_INFO, (task_info_t)&basic_info, &t_info_count);
 
        if (status != KERN_SUCCESS)
        {
            NSLog(@"%s(): Error in task_info(): %s", __FUNCTION__, strerror(errno));
            return -1;
        }
 
        return (unsigned int)basic_info.resident_size;
    }
}