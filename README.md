
Command Query Responsibility Segregation (CQRS)
https://devwithjosh.com/cqrs-and-event-sourcing-in-c-net-part-1
https://devwithjosh.com/cqrs-and-event-sourcing-in-c-net-part-2
--------------------------------------------------------------------------------------
 Install-packege Ardalis.GuardClauses
  Install-packege AutoMapper" Version="13.0.1" />
 Install-packegeMicrosoft.EntityFrameworkCore" Version="9.0.0" />
  Install-packegeMicrosoft.EntityFrameworkCore.Design" Version="9.0.0">
  Install-packegeMicrosoft.EntityFrameworkCore.Sqlite  Version="9.0.0" />
 Install-packegeMicrosoft.EntityFrameworkCore.Tools" Version="9.0.0 
Install-packege StackExchange.Redis  --Version="2.8.24""  
   
----------------------------------------------------------------------------------

Add-Migration InitialCreate
Update-Database

docker pull focker.ir/rabbitmq:3-management
docker pull focker.ir/redis/redis-stack

docker tag focker.ir/rabbitmq:3-management  rabbitmq:3-management
docker tag focker.ir/redis/redis-stack:latest redis/redis-stack:latest

docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
http://localhost:15672
docker run -d --name redis-stack-server -p 6379:6379 -p 8082:8081 redis/redis-stack
http://localhost:8082
docker ps

در این پروژه  ما خودمان پیاده سازی داشتیم  و از مدیتور استفاده نکردیم پروژه زیر  از این تردپارتی استفاده شده  
CQRS-and-mediatr
------------------------------------------------------------------------------------------------------
هدف ICommand و ICommandHandler
تفکیک نگرانی ها:

ICCommand: نشان دهنده قصد انجام یک عملیات یا دستور خاص است و تمام داده های مورد نیاز برای انجام آن عملیات (مانند ورودی ها یا پارامترها) را در خود محصور می کند.
ICommandHandler: مسئول اجرای منطق مرتبط با یک دستور خاص است. این جداسازی اجازه می دهد تا مرزهای واضحی در کد شما ایجاد شود و درک و حفظ آن آسان تر شود.
کپسوله سازی منطق فرمان:

با ICommand می توانید تمام داده های لازم برای اجرای یک عملیات را کپسوله کنید. این به حفظ یکپارچگی فرمان کمک می کند زیرا تمام اطلاعات مربوطه با هم منتقل می شوند.
سپس ICommandHandler مربوطه می تواند تنها بر اجرای دستور تمرکز کند که پیچیدگی هر جزء را کاهش می دهد.


-------------------------------------------------------------------------------------------------------

    public class UpdateOrderCommand : ICommand
    {
        public Order Order { get;set; }

        public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
        {
            private readonly IWriteOrderRepository repository;

            public CreateOrderCommandHandler(IWriteOrderRepository repository)
            {
                this.repository = repository;
            }
            public async Task HandleAsync(CreateOrderCommand command)
            {
                Guard.Against.Null(command);

                await repository.CreateOrderAsync(command.Order);
            }
        }

    }
    --------------------------------------------------------------------------------------------------

    تفاوت های اصلی بین Redis و Redis Stack را می توان به شرح زیر خلاصه کرد:
1. عملکرد اصلی
Redis: این ذخیره‌گاه داده‌های کلید-مقدار اصلی و منبع باز در حافظه است. از ساختارهای داده مختلف مانند رشته ها، هش ها، لیست ها، مجموعه ها و مجموعه های مرتب شده پشتیبانی می کند. Redis در درجه اول برای کش کردن، ذخیره سازی جلسه و تجزیه و تحلیل بلادرنگ استفاده می شود.
Redis Stack: این بر اساس عملکرد اصلی Redis با افزودن مجموعه‌ای از ماژول‌ها است که قابلیت‌های آن را گسترش می‌دهد. این ماژول‌ها قابلیت‌های پیشرفته پردازش و جستجوی داده‌ها را فراتر از موارد استاندارد استفاده می‌کنند.
2. ماژول های گنجانده شده است
Redis: استاندارد Redis با ماژول های پیشرفته خارج از جعبه عرضه نمی شود.
Redis Stack: شامل چندین ماژول قدرتمند مانند:
RediSearch: برای قابلیت جستجوی متن کامل.
RedisJSON: برای ذخیره، پرس و جو و دستکاری داده های JSON.
RedisGraph: برای مدیریت داده های گراف و اجرای پرس و جوهای گراف پیچیده.
RedisTimeSeries: برای ذخیره و جستجوی داده های سری زمانی.
RedisBloom: برای ساختارهای داده احتمالی، مانند فیلترهای بلوم.
3. موارد استفاده
Redis: اغلب برای ذخیره سازی ساده، کارگزاری پیام در زمان واقعی و نیازهای اولیه ذخیره سازی داده استفاده می شود.
Redis Stack: برای برنامه‌های پیچیده‌تری که به قابلیت‌هایی مانند جستجوی متن کامل، مدل‌سازی داده‌های پیشرفته (مانند نمودارها)، و مدیریت داده‌های JSON نیاز دارند، هدف قرار می‌گیرد، که آن را برای تجزیه و تحلیل بلادرنگ، سیستم‌های گزارش‌گیری و برنامه‌هایی که نیاز به ترکیب داده‌ها دارند مناسب می‌کند. تایپ کرده و آنها را به طور موثر جستجو کنید.
4. نصب و راه اندازی
Redis: نصب ساده است، به سادگی به سرور اصلی Redis نیاز دارد.
Redis Stack: نصب معمولاً شامل راه‌اندازی اضافی برای گنجاندن ماژول‌های مورد نظر است که می‌تواند از طریق Docker یا سایر مدیران بسته‌های متناسب با Redis Stack انجام شود.          
5. ویژگی های عملکرد
Redis: عملکرد بالا و تاخیر کم برای عملیات استاندارد.
Redis Stack: عملکرد می تواند بسته به ماژول مورد استفاده و پیچیدگی پرس و جوهای اجرا شده متفاوت باشد، اما به طور کلی برای عملیات بلادرنگ کارآمد باقی می ماند.
نتیجه گیری
به طور خلاصه، Redis برای ذخیره سازی داده های استاندارد و نیازهای حافظه پنهان بسیار کارآمد است، در حالی که Redis Stack عملکردهای بهبود یافته ای را برای برنامه هایی که به قابلیت های مدیریت داده و جستجوی غنی تر نیاز دارند، ارائه می دهد. اگر روی پروژه ای کار می کنید که می تواند از ویژگی هایی مانند جستجوی متن کامل یا انواع داده های پیچیده بهره مند شود، Redis Stack انتخاب مناسبی خواهد بود.
