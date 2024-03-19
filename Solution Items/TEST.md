**Bağımsızlık**: Her test bağımsız olmalıdır. Bir testin sonucu, diğer testlerin sonuçlarından etkilenmemelidir. Bu, testlerin herhangi bir sırayla çalıştırılabileceği ve aynı sonuçları üreteceği anlamına gelir.

**Tekrarlanabilirlik**: Testler herhangi bir ortamda tekrar tekrar çalıştırılabilmelidir ve her seferinde aynı sonucu vermelidir. Bu, testlerin dış sistemlerden veya dış durumlardan etkilenmemesi gerektiği anlamına gelir.

**Otomatikleştirilebilirlik**: Testler, manuel müdahale gerektirmeden otomatik olarak çalıştırılabilmelidir. Bu, test süreçlerinin hızlı ve verimli bir şekilde gerçekleştirilmesini sağlar.

**Anlamlılık**: Testler, test edilen özelliğin veya davranışın ne olduğunu açıkça belirtmelidir. Test başarısız olduğunda, sorunun ne olduğunu kolayca anlamak mümkün olmalıdır.

**Kapsamlılık**: Testler, yazılımın önemli davranışlarını ve özelliklerini kapsamlı bir şekilde test etmelidir. Bu, pozitif ve negatif test durumlarını, sınır durumlarını ve hata durumlarını içerir.

**Hızlı Yürütülebilirlik**: Testler hızlı bir şekilde çalıştırılabilmelidir, böylece geliştirme sürecini yavaşlatmazlar ve sürekli entegrasyon (CI) ve sürekli teslimat (CD) süreçlerine entegre edilebilirler.

**Sürdürülebilirlik**: Test kodu, uygulama kodu kadar temiz ve sürdürülebilir olmalıdır. Testler zamanla uygulama ile birlikte evrilmeli ve gerektiğinde kolayca güncellenebilmelidir.



**Arrange**
"`Arrange`" aşaması, testin çalıştırılması için gerekli olan her şeyin hazırlanmasını içerir. Bu, test edilecek nesnelerin ve bağımlılıklarının oluşturulması, gerekli verilerin ayarlanması ve test ortamının konfigüre edilmesi gibi işlemleri kapsar. Bu aşama, testin gerçekleştirileceği koşulları kurar ve testin başarılı bir şekilde çalışabilmesi için gerekli olan her şeyin yerinde olduğundan emin olur.

Örneğin, bir veritabanı işlemi test ediliyorsa, "`Arrange`" aşamasında test veritabanı bağlantısı kurulur, test için gerekli veriler veritabanına eklenir ve test edilecek metodun bir örneği oluşturulur.

**Act**
"`Act`" aşaması, testin asıl eylemini gerçekleştirir. Bu, test senaryosunun merkezinde yer alan eylemdir; örneğin, bir metodun çağrılması veya bir işlemin gerçekleştirilmesi. "`Act`" aşaması, "`Arrange`" aşamasında hazırlanan nesneler üzerinde işlem yapar ve bir sonuç üretir. Bu sonuç daha sonra "`Assert`" aşamasında değerlendirilir.

**Assert**
"`Assert`" aşaması, "`Act`" aşamasında elde edilen sonucun beklenen sonuçla karşılaştırılmasını içerir. Bu aşama, testin başarılı olup olmadığını belirler. Eğer test sonucu beklenen değerlerle eşleşiyorsa, test başarılı olarak kabul edilir; aksi takdirde, test başarısızdır ve hata analizi için bilgi sağlar.



# NUnit Test Attribute'leri

| Attribute                           | Açıklama                                                                                                                                                                                                                   |
|-------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Apartment Attribute`               | Testin belirli bir apartment'ta (STA veya MTA gibi) çalıştırılması gerektiğini belirtir.                                                                                                                                    |
| `Author Attribute`                  | Test yazarının adını sağlar.                                                                                                                                                                                               |
| `CancelAfter Attribute`             | Test vakaları için milisaniye cinsinden bir zaman aşımı değeri sağlar.                                                                                                                                                     |
| `Category Attribute`                | Test için bir veya daha fazla kategori belirtir.                                                                                                                                                                           |
| `Combinatorial Attribute`           | Sağlanan değerlerin tüm mümkün kombinasyonları için test vakaları üretir.                                                                                                                                                  |
| `Culture Attribute`                 | Bir testin veya fixture'ın hangi kültürler için çalıştırılması gerektiğini belirtir.                                                                                                                                       |
| `Datapoint Attribute`               | Teoriler için veri sağlar.                                                                                                                                                                                                 |
| `DatapointSource Attribute`         | Teoriler için veri kaynağı sağlar.                                                                                                                                                                                         |
| `DefaultFloatingPointTolerance Attribute` | Testin, float ve double karşılaştırmaları için belirtilen toleransı varsayılan olarak kullanması gerektiğini belirtir.                                                                                                      |
| `Description Attribute`             | Bir Test, TestFixture veya Assembly'e açıklayıcı metin uygular.                                                                                                                                                            |
| `Explicit Attribute`                | Bir testin sadece açıkça çalıştırılması gerektiğini belirtir.                                                                                                                                                              |
| `FixtureLifeCycle Attribute`        | Bir test vakası için her seferinde yeni bir test fixture örneği oluşturulmasını sağlayarak bir fixture'ın yaşam döngüsünü belirtir. Test vakası paralelliği önemli olduğunda kullanışlıdır.                                |
| `Ignore Attribute`                  | Bir testin belirli bir nedenle çalıştırılmaması gerektiğini belirtir.                                                                                                                                                      |
| `LevelOfParallelism Attribute`      | Assembly seviyesinde paralellik seviyesini belirtir.                                                                                                                                                                       |
| `MaxTime Attribute`                 | Bir test vakasının başarılı olması için maksimum süreyi milisaniye cinsinden belirtir.                                                                                                                                     |
| `NonParallelizable Attribute`       | Testin ve onun altındakilerin paralel olarak çalıştırılamayacağını belirtir.                                                                                                                                               |
| `NonTestAssembly Attribute`         | Assembly'nin NUnit framework'ünü referans aldığını ancak test içermediğini belirtir.                                                                                                                                       |
| `OneTimeSetUp Attribute`            | Çocuk testlerden önce bir kez çağrılacak metotları tanımlar.                                                                                                                                                               |
| `OneTimeTearDown Attribute`         | Tüm çocuk testlerden sonra bir kez çağrılacak metotları tanımlar.                                                                                                                                                          |
| `Order Attribute`                   | Süslenmiş testin, içinde bulunduğu fixture veya suite içindeki çalışma sırasını belirtir.                                                                                                                                  |
| `Pairwise Attribute`                | Sağlanan değerlerin tüm mümkün çiftler için test vakaları üretir.                                                                                                                                                          |
| `Parallelizable Attribute`          | Testin ve/veya onun altındakilerin paralel olarak çalıştırılıp çalıştırılamayacağını belirtir.                                                                                                                              |
| `Platform Attribute`                | Bir testin veya fixture'ın hangi platformlarda çalıştırılması gerektiğini belirtir.                                                                                                                                        |
| `Property Attribute`                | Herhangi bir test vakası veya fixture üzerinde adlandırılmış özellikler ayarlamayı sağlar.                                                                                                                                 |
| `Random Attribute`                  | Parametreli bir test için rastgele değerlerin argüman olarak üretilmesini belirtir.                                                                                                                                        |
| `Range Attribute`                   | Parametreli bir test için bir değer aralığını argüman olarak belirtir.                                                                                                                                                     |
| `Repeat Attribute`                  | Süslenmiş metotun birden çok kez çalıştırılması gerektiğini belirtir.                                                                                                                                                      |
| `RequiresThread Attribute`          | Bir test metodu, sınıfı veya assembly'nin ayrı bir thread üzerinde çalıştırılması gerektiğini belirtir.                                                                                                                    |
| `Retry Attribute`                   | Bir test başarısız olursa, maksimum belirli bir sayıda tekrar çalıştırılmasını sağlar.                                                                                                                                     |
| `Sequential Attribute`              | Sağlanan değerleri sırasıyla kullanarak, ek kombinasyonlar oluşturmadan test vakaları üretir.                                                                                                                              |
| `SetCulture Attribute`              | Bir testin süresi boyunca geçerli Kültür'ü ayarlar.                                                                                                                                                                        |
| `SetUICulture Attribute`            | Bir testin süresi boyunca geçerli UI Kültür'ü ayarlar.                                                                                                                                                                     |
| `SetUp Attribute`                   | Her test metodu öncesinde çağrılacak bir TestFixture metodu belirtir.                                                                                                                                                      |
| `SetUpFixture Attribute`            | Bir namespace'deki tüm test fixture'ları için bir kez kurulum veya temizleme metotları içeren bir sınıfı işaretler.                                                                                                        |
| `SingleThreaded Attribute`          | Tüm testlerin aynı thread üzerinde çalışması gereken bir fixture'ı işaretler.                                                                                                                                              |
| `TearDown Attribute`                | Her test metodu sonrasında çağrılacak bir TestFixture metodu belirtir.                                                                                                                                                     |
| `Test Attribute`                    | Bir TestFixture içindeki bir metodu test olarak işaretler.                                                                                                                                                                 |
| `TestCase Attribute`                | Parametrelerle bir metodu test olarak işaretler ve satır içi argümanlar sağlar.                                                                                                                                            |
| `TestCaseSource Attribute`          | Parametrelerle bir metodu test olarak işaretler ve bir argüman kaynağı sağlar.                                                                                                                                            
`TestFixture Attribute`               | Bir sınıfı test fixture olarak işaretler ve isteğe bağlı olarak inline constructor argümanları sağlayabilir. Bu attribute, bir test sınıfının içindeki test metodlarının bir grup olarak ele alınmasını sağlar ve genellikle setup ve teardown metodları ile birlikte kullanılır. |