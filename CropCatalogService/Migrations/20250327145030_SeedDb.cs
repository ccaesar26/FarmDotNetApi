using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CropCatalogService.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CropCatalogEntries",
                columns: new[] { "Id", "BinomialName", "DaysToFirstHarvest", "DaysToLastHarvest", "Description", "HarvestSeasonEnd", "HarvestSeasonStart", "ImageLink", "IsPerennial", "MaxMonthsToBearFruit", "MinMonthsToBearFruit", "Name", "SowingMethod", "SunRequirements", "WikipediaLink" },
                values: new object[,]
                {
                    { new Guid("02734545-6762-4d3a-8474-4fb212dffe71"), "Vitis", null, null, "Grapes are the clustered fruit of deciduous, perennial woody vines in the Vitis genus. The majority of both table and wine grapes are cultivars of the European grapevine, Vitis vinifera, which is native to the Mediterranean and Central Asia. Other species include Vitis labrusca, the North American table and grape juice species, which is more cold-hardy, and Vitis amurensis, the most important Asian species. There are over 10,000 varieties of wine grapes. Table grape cultivars have large, seedless fruit with thin skin. Wine grapes have smaller fruit with seeds, thicker skin (most of the aroma in wine comes from the skin), and a higher sugar content. Grapes are propagated through cuttings because seeds do not reliably yield the same type of plant as their parent. Most varieties are self-fertile, but some may need another plant for pollination. All grapes need to be trained to a support so they grow upward - this reduces disease risk and facilitates cultivation and harvest. Plants should not be allowed to produce fruit in the first few years - the plant's energy needs to be focused on establishing roots and vines. Grapes only produce fruit on new growth, or canes, and need to be pruned each year in late winter when the vines are still dormant. Heavy pruning results in higher fruit yields. Most grapes need about 150 chilling hours at temperatures below 10°C. Grapes need to be ripened on the vine.", new DateOnly(2000, 10, 31), new DateOnly(2000, 8, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b7d46fe8d750003000413.jpg?1466662209", true, 36, 24, "Grape", "Plant dormant, bare-root cuttings or vines in early spring", "Full Sun", "https://en.wikipedia.org/wiki/Grape" },
                    { new Guid("0314a200-03d1-4470-b806-2b3dba41b4f9"), "Cannabis sativa", 70, 120, "Hemp is a variety of Cannabis sativa that is grown for industrial uses. It can be made into paper, textiles, clothing, rope, food, animal feed, biofuel, and biodegradable plastics. Hemp has a lower concentration of THC and a higher concentration of CBD than Marijuana (the other dominant variety of Cannabis sativa), which means it has little to no psychoactive effects. Despite this, the legality of hemp cultivation varies throughout the U.S. Hemp is low maintenance, fast growing, and an extremely efficient weed suppressor. It uses significantly less water and land than cotton to produce similar textile yields.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59287947f9f0b200040000c2.jpg?1495824709", false, null, null, "Hemp", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Hemp" },
                    { new Guid("05736025-5fa0-46ac-b48a-f5903a779bd7"), "Allium porrum", 120, 180, "The leek is a vegetable, a cultivar of Allium ampeloprasum, the broadleaf wild leek. The edible part of the plant is a bundle of leaf sheaths that is sometimes erroneously called a stem or stalk. Historically, many scientific names were used for leeks, but they are now all treated as cultivars of A. ampeloprasum. The name 'leek' developed from the Anglo-Saxon word \"leac\". Two closely related vegetables, elephant garlic and kurrat, are also cultivars of A. ampeloprasum, although different in their uses as food. The onion and garlic are also related, being other species of the genus Allium.\nLeeks have thick blue-green foliage. The bundle of white leaf sheaths has a mild onion taste and can be blanched, steamed, braised, or grilled and used in soups, stews, omelet fillings, and more. The leaves can be used to make stock.\n\nPlant their leeks in autumn, and they should fatten up in time for winter picking. Plant them early to ensure they have enough time to grow before winter. Leeks take up to six months to mature after transplanting.\n\nNewer cultivars have quicker maturity  three to four months. Maturity is often affected by temperature, available nutrients and water. \n\nLeeks need a soil that is rich in organic matter. Dig in compost or manure two weeks before planting. Add fertiliser every few weeks\nLeeks like aged manure, especially chicken manure, and worm castings, another excellent source of nutrients.\n\nPlant seedlings in full sun, with moist but well-drained deep soil. Raised beds are ideal. Water young plants frequently.\n\nThe white part of the leek is edible; the green is not. You can blanch the stems to increase the proportion of stem thats edible and to sweeten the taste. Do this to fully grown leeks. Tie a paper collar around each stem and gently hill the earth up around the stem. Be careful not to get soil between the paper collar and stem as the leek may rot.\nAs the plants grow, add another collar above the first one and hill up more soil. Be aware that slugs and snails can hide in the paper collars\n\nHarvest the leeks when stems are around 2.5cm in diameter. Dig carefully around the leek and lift with a garden fork. Do not pull, as the leek is likely to break.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/591b57fd1faea7000400000a.jpg?1494964215", false, null, null, "Leek", "Direct seed or transplant seedlings", "Full Sun", "https://en.wikipedia.org/wiki/Leeks" },
                    { new Guid("05cbba0c-7cf7-4f86-9457-6dafbe00c7cd"), "Brassica oleracea", 60, 100, "Cabbage is a member of the Brassica family and related to kale, broccoli, brussels sprouts, and cauliflower. It's dense, layered heads grow on stalks and are surrounded by looser outer leaves. It's leaves can be green, white, or purple in color, and smooth or crinkly in texture. Depending on the variety, the head can be round, oblong, or flat. Cabbage prefers cooler temperatures and is best planted in the spring or fall.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5928732ef9f0b200040000c0.jpg?1495823146", false, null, null, "Cabbage", "Direct seed indoors, transplant seedlings outside after hardening off.", "Full Sun", "https://en.wikipedia.org/wiki/Cabbage" },
                    { new Guid("0695224d-7a00-4730-8786-c712113abbe5"), "Pisum sativum", 50, 80, "Sweet, edible pods/seeds", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b8edefe8d75000300043c.jpg?1466666716", false, null, null, "Pea", "direct", "Full Sun", "https://en.wikipedia.org/wiki/Pea" },
                    { new Guid("0951aace-1a45-4f9c-bef2-a07e850a377a"), "Prunus avium", null, null, "A cherry is the fruit of many plants of the genus Prunus, and is a fleshy drupe. Commercial cherries are obtained from cultivars of several species, such as the sweet Prunus avium and the sour Prunus cerasus.", new DateOnly(2000, 6, 30), new DateOnly(2000, 5, 15), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dc3a137323900036c0100.jpg?1428013982", true, 60, 36, "Cherry", null, "Full Sun", "https://en.wikipedia.org/wiki/Cherry" },
                    { new Guid("0d0bb89a-9792-4d25-a7f0-f151a6fd77e6"), "anethum graveolens", 30, 50, "Dill is a fragrant herb commonly used in cooking, especially for its fresh leaves and seeds. It has feathery, delicate green leaves and produces yellow, umbrella-like flower clusters. The plant is known for its unique flavor, often used in pickling, salad dressings, and as a seasoning for fish and potatoes. It attracts beneficial insects, such as pollinators, and can be grown in both home gardens and containers.", null, null, "/assets/baren_field_square-4a827e5f09156962937eb100e4484f87e1e788f28a7c9daefe2a9297711a562a.jpg", false, null, null, "Dill", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Dill" },
                    { new Guid("0edbe025-32e4-4b69-b9d3-7bef157ae72f"), "Malus pumila", null, null, "The apple is a deciduous tree in the Rose family grown for it's sweet fruit. The apple originated in Central Asia and has spread across the world. There are now over 7,500 cultivars bred for a variety of climates and characteristics. Apples are propagated through grafting because seeds do not breed true.", new DateOnly(2000, 10, 31), new DateOnly(2000, 9, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5939df7401f8790004000005.jpg?1496964975", true, 60, 36, "Apple", "Bare root tree", "Full Sun", "https://en.wikipedia.org/wiki/Apple" },
                    { new Guid("0ee9c322-ce79-49e3-873f-1d0c437c1514"), "Cucurbita pepo", 90, 120, "Pumpkins are squash cultivars that are round to oval in shape with thick, slightly ribbed skin that varies from deep yellow to orange in color. Their flesh ranges from yellow to gold and has large seeds. Like other members of the Cucurbitaceae family, they grow on sprawling vines. Different varieties of pumpkins are grown for food or decoration.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/54b4aa886130650002020000.jpg?1421126278", false, null, null, "Pumpkin", "Direct seed indoors (and transplant outside after seedlings are hardened off) or outdoors", "Full Sun", "http://en.wikipedia.org/wiki/Pumpkin" },
                    { new Guid("145472af-603c-4cf4-bbba-c7b557a1c8ce"), "Brassica napus", 90, 120, "Rapeseed, or Oilseed Rape, is an annual or biennial flowering plant in the Brassica family that is grown for its oil-rich seeds. The seeds are used to create vegetable oil, feed livestock, and produce biodiesel. Canola is a specific cultivar. Rapeseed has an upright growth habit, flattened leaves, yellow flowers, and sickle-shaped seed pods with tiny black seeds. The oil of wild varieties is high in erucic acid and can be toxic, but cultivated varieties are nearly free of erucic acid. Young leaves can be eaten as kale-like greens. Rapeseed can become invasive as it naturalizes in the wild easily. It is tolerant of saline conditions.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5a13692e956ccf000400000f.jpg?1511221533", false, null, null, "Rapeseed", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Brassica_napus" },
                    { new Guid("20190095-7d39-4ef4-a687-5e98c09af0f7"), "Citrullus lanatus", 70, 90, "The watermelon is a species of melon that produces round or oblong fruits with thick skin and sweet, watery flesh. It is a special kind of berry with a hard rind and no internal division, botanically known as a \"pepo.\" The rind is usually dark green with light-green stripes. The flesh can be red or yellow. Like other melons and members of the Cucurbitaceae family, the watermelon grows on sprawling vines.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/591e1e22dfdcf50004000001.jpg?1495146011", false, null, null, "Watermelon", "Direct seed into soil or peat pots (and transplant pots directly into soil after hardening off)", "Full Sun", "https://en.wikipedia.org/wiki/Watermelon" },
                    { new Guid("20fc5890-3dd7-479a-be36-5502ce8f628b"), "Solanum melongena", 70, 90, "Eggplants commonly are egg-shaped with glossy black skin, but can come in a variety of other shapes and colors. They can be white, yellow, and pale to deep purple. Some are as small as goose eggs. The 'Rosa Bianca' cultivar is squat and round, while Asian cultivars can be long and thin. Eggplant stems are often spiny and their flowers range from white to purple. \n\nTheir flesh is generally white with a meaty texture and small seeds in the center. They are delicious grilled, roasted, in soups and stews, and breaded and fried.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b79ddfe8d75000300038a.jpg?1466661339", false, null, null, "Eggplant", "Sow seeds indoors and transplant out, or plant nursery seedlings", "Full Sun", "https://en.wikipedia.org/wiki/Eggplant" },
                    { new Guid("283d5ae8-a13b-4289-bdbb-ace381d97925"), "Brassica napus", 90, 120, "Rapeseed, or Oilseed Rape, is an annual or biennial flowering plant in the Brassica family that is grown for its oil-rich seeds. The seeds are used to create vegetable oil, feed livestock, and produce biodiesel. Canola is a specific cultivar. Rapeseed has an upright growth habit, flattened leaves, yellow flowers, and sickle-shaped seed pods with tiny black seeds. The oil of wild varieties is high in erucic acid and can be toxic, but cultivated varieties are nearly free of erucic acid. Young leaves can be eaten as kale-like greens. Rapeseed can become invasive as it naturalizes in the wild easily. It is tolerant of saline conditions.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5a13692e956ccf000400000f.jpg?1511221533", false, null, null, "Rapeseed", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Brassica_napus" },
                    { new Guid("2b6ef592-6181-47c0-9f05-fe0d04291671"), "Brassica oleracea", 60, 100, "Broccoli has large flower heads known as \"crowns\" that are green to blue-green in color, grouped tightly together atop a thick stem, and surrounded by leaves. Broccoli resembles cauliflower, a different cultivar in its species. It thrives in cool weather.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/54b4b5ea61306500020b0000.jpg?1421129190", false, null, null, "Broccoli", "Sow seeds indoors and transplant outside", "Full Sun", "https://en.wikipedia.org/wiki/Broccoli" },
                    { new Guid("33a3bde7-f50b-4f19-877e-264051eed218"), "Zea mays", 60, 100, "Corn is a large grain plant, or tall grass, first domesticated about 10,000 years ago by indigenous peoples in Southern Mexico. The leafy stalk produces ears after pollination. Depending on the variety, the corn can be eaten fresh, or dried and ground into cornmeal.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b77b7fe8d750003000300.jpg?1466660787", false, null, null, "Corn", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Corn" },
                    { new Guid("35bfef24-94e1-4b92-b5fe-f4577bed893e"), "Triticum aestivum", 120, 180, "Wheat is a grass that is grown for its seed. Many species of wheat make up the genus Triticum, with the most widely grown species being Common Wheat (Triticum aestivum). Wheat can be divided into two main groups: Winter (planted in fall and harvested in the spring or summer) and Spring (planted in spring and harvested in fall). Winter and Spring can then be divided into 1) Soft wheat (low in gluten, used for pastries and crackers), 2) Hard wheat (high in gluten, used for bread), and 3) Durum wheat (used for pasta). Winter wheat should be planted 6-8 weeks before the soil freezes to allow time for good root development. Spring wheat is planted as early as the ground can be worked in the spring.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dc42b37323900036e0100.jpg?1428014119", false, null, null, "Wheat", "Direct seed into furrows. Broadcast or use grain drill. Rake in, roll soil to firm the bed.", "Full Sun", "https://en.wikipedia.org/wiki/Wheat" },
                    { new Guid("35f0f98b-f664-40b8-b881-86966c683105"), "Rubus idaeus", null, null, "Raspberries are a perennial plant with erect to trailing canes that often have spines or thorns. The plants produce fruit in their second year of growth, but some \"primocane\" varieties exist that flower and fruit their first year. Canes are light green to blue in hue with alternate, compound leaves. Fruits are sweet, many-seeded, and hollow.", new DateOnly(2000, 8, 31), new DateOnly(2000, 6, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dc30837323900036b0100.jpg?1428013830", true, 24, 12, "Raspberry", "Transplant from roots or juvenile plants.", "Partial Sun", "https://en.wikipedia.org/wiki/Rubus_idaeus" },
                    { new Guid("3a887d21-6594-4d41-86c8-5abde19ceb5c"), "Oryza sativa", 90, 180, "Rice is the seed of the grass species Oryza sativa. It is a cereal grain that can come in many shapes, colors, and sizes. Rice requires ample water and is labor-intensive to cultivate. Water-controlling terrace systems enable it to be grown almost anywhere. The traditional method of cultivating rice is to flood the fields during or after setting the young seedlings. Rice can be grown without flooding, but flooding simplifies weed and pest control.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58e330a94bca6f00040000b8.jpg?1491284134", false, null, null, "Rice", "Seedlings", "Full Sun", "https://en.wikipedia.org/wiki/Rice" },
                    { new Guid("3d80ffef-3318-4596-8feb-36d98ee5fe38"), "Helianthus annuus", 70, 100, "Sunflowers are large flowers with bright to deep yellow ray florets surrounding a large circular grouping of disc florets that mature into seeds. Sunflowers are grown for ornamental purposes, cut flowers, or their edible seeds. They can reach heights of 300cm or more.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/550c827f3730310003ea0000.jpg?1426883198", false, null, null, "Sunflower", "Direct seed outdoors after last frost in groups of 2-3, thin to 1 plant when true leaves appear", "Full Sun", "https://en.wikipedia.org/wiki/Sunflower" },
                    { new Guid("40c8310c-3f72-47d1-bc06-4de6e818d0bc"), "Juglans regia", null, null, "The English, or Common, Walnut is a large deciduous tree native to Eastern Europe, the Himalayas, and southwest China. Like other walnut species, it has a broad crown and short trunk and needs full sunlight to grow well. The English Walnut is the most commonly available commercial nut, but it is sometimes confused with the Black Walnut, which is native to North America. English Walnuts have thinner shells that are easier to crack and more mild-flavored nuts than Black Walnuts. They have larger leaflets than Black Walnuts and their bark is gray-black and deeply grooved. English Walnuts grow where the temperature range is 7.0 to 21.1°C. When fully dormant, they can survive temperatures of -24 to -27° C. Temperatures below -29° C will kill the tree. English Walnuts need 500-1500 hours at temperatures below 7° C a year. Walnuts are dioecious, meaning they have both male and female flowers on the same tree and are therefore self-pollinating. However, the flowers do not always bloom at the same time. This problem can be solved by planting another walnut cultivar upwind whose catkins shed pollen at the same time the female flowers of the primary tree are open. Walnuts, like Hickories, contain a chemical called juglone, which inhibits the growth of many other plants. They are also difficult to grow other plants with because mature trees use a lot of water and shade large areas around them. The plants listed as companion plants can tolerate juglone. Grafted trees will begin to produce fruit one year after transplanting. Trees grown from seed will take 8-10 years to begin bearing fruit and may not bear true to the parent if trees have been cross-pollinated. Direct seeding is most effective when seeds are sown outdoors in autumn. Winter temperatures will break the seed's dormancy. If starting from seed indoors, pre-soak, scarify, and cold-stratify before planting to break dormancy.", new DateOnly(2000, 11, 30), new DateOnly(2000, 9, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5a2711f179642c000400000b.jpg?1512509934", true, 120, 60, "English Walnut", "Transplant grafted sapling or direct seed", "Full Sun", "https://en.wikipedia.org/wiki/Juglans_regia" },
                    { new Guid("41e53c61-8824-4e6b-8842-6f2e60567a60"), "Humulus lupulus", null, null, "Hops are the flowers (also known as seed cones or strobiles) of the hop plant. They are primarily used as a flavoring and stability agent in beer, giving it a bitter, zesty, or citric flavor, but have additional medicinal and beverage uses. The hop plant is a vigorous climber that can grow to heights of 10m. It is trained to grow up trellises made from strings or wires.", new DateOnly(2000, 10, 31), new DateOnly(2000, 8, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b8139fe8d750003000418.jpg?1466663223", true, 36, 24, "Hops", "Transplant seedlings or rhizomes/rootstock", "Full Sun", "http://en.wikipedia.org/wiki/Hops" },
                    { new Guid("44274b8e-c19f-49c6-9d32-db1356d2066d"), "Triticum aestivum", 120, 180, "Wheat is a grass that is grown for its seed. Many species of wheat make up the genus Triticum, with the most widely grown species being Common Wheat (Triticum aestivum). Wheat can be divided into two main groups: Winter (planted in fall and harvested in the spring or summer) and Spring (planted in spring and harvested in fall). Winter and Spring can then be divided into 1) Soft wheat (low in gluten, used for pastries and crackers), 2) Hard wheat (high in gluten, used for bread), and 3) Durum wheat (used for pasta). Winter wheat should be planted 6-8 weeks before the soil freezes to allow time for good root development. Spring wheat is planted as early as the ground can be worked in the spring.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dc42b37323900036e0100.jpg?1428014119", false, null, null, "Wheat", "Direct seed into furrows. Broadcast or use grain drill. Rake in, roll soil to firm the bed.", "Full Sun", "https://en.wikipedia.org/wiki/Wheat" },
                    { new Guid("4856d993-9d6c-4cea-94c6-bf6d5b71ad44"), "Brassica oleracea", 60, 100, "Cabbage is a member of the Brassica family and related to kale, broccoli, brussels sprouts, and cauliflower. It's dense, layered heads grow on stalks and are surrounded by looser outer leaves. It's leaves can be green, white, or purple in color, and smooth or crinkly in texture. Depending on the variety, the head can be round, oblong, or flat. Cabbage prefers cooler temperatures and is best planted in the spring or fall.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5928732ef9f0b200040000c0.jpg?1495823146", false, null, null, "Cabbage", "Direct seed indoors, transplant seedlings outside after hardening off.", "Full Sun", "https://en.wikipedia.org/wiki/Cabbage" },
                    { new Guid("4ab704aa-7022-414b-a4fc-31b6e1b11ff6"), "Phaseolus vulgaris", 50, 100, null, null, null, null, false, null, null, "Bean", null, null, "https://en.wikipedia.org/wiki/Common_bean" },
                    { new Guid("4c028591-037a-4d2c-ae83-0240f634affa"), "Allium cepa", 90, 180, "Onions are bulbous vegetables used in a wide range of culinary dishes, prized for their pungency when raw and sweetness when cooked. They come in different colors, including white, yellow, and red.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/56857be71083470003000000.jpg?1451588582", false, null, null, "Onion", "Start indoors", "Full Sun", "https://en.wikipedia.org/wiki/Onions" },
                    { new Guid("4d1a2914-e84b-479b-a1b7-ea365aa8b73f"), "Nicotiana tabacum", 90, 130, null, null, null, null, false, null, null, "Tobacco", null, null, "https://en.wikipedia.org/wiki/Tobacco" },
                    { new Guid("4e634558-3ec4-40bd-90ea-0354103c8ba1"), "Solanum lycopersicum", 50, 85, "The tomato is the fruit of the tomato plant, a member of the Nightshade family (Solanaceae). The fruit grows on a small compact bush.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5dc3618ef2c1020004f936e4.jpg?1573085580", false, null, null, "Tomato", "Direct seed indoors, transplant seedlings outside after hardening off", "Full Sun", "https://en.wikipedia.org/wiki/Tomato" },
                    { new Guid("508f17ec-2ce1-447c-8665-f326e0ce1f9a"), "Allium sativum", 150, 240, null, null, null, null, false, null, null, "Garlic", null, null, "https://en.wikipedia.org/wiki/Garlic" },
                    { new Guid("512b47f4-20ab-4d22-9e47-af7d363f377f"), "Solanum lycopersicum", 50, 85, "The tomato is the fruit of the tomato plant, a member of the Nightshade family (Solanaceae). The fruit grows on a small compact bush.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5dc3618ef2c1020004f936e4.jpg?1573085580", false, null, null, "Tomato", "Direct seed indoors, transplant seedlings outside after hardening off", "Full Sun", "https://en.wikipedia.org/wiki/Tomato" },
                    { new Guid("52c5dffe-4732-4fb4-bc3c-5aa4d13811fa"), "Cicer arietinum", 90, 150, "Chickpeas are a cool-season annual legume in the Fabaceae family. They are native to the Middle East and Mediterranean. They can be sprouted indoors in 2-3 days for salads and fresh eating, or grown outside for their seeds. The upright plants do not require staking and grow to a height of about 50cm. They have small, feathery leaves. Flowers are white with blue, violet, or pink veins and form 2.5cm long oblong pods with 1-2 large, cream-colored seeds. Chickpeas can be picked while green and eaten fresh like snap peas, but they are most commonly harvested as a dried bean crop. Chickpeas are high in protein and drought-tolerant. They benefit from an inoculant at planting.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59d6761c63be110004000003.jpg?1507227163", false, null, null, "Chickpea", "Direct seed indoors, transplant seedlings outside after hardening off", "Full Sun", "https://en.wikipedia.org/wiki/Chickpea" },
                    { new Guid("57b1f5cd-5fe0-469a-a343-7f1a49c28e60"), "Humulus lupulus", null, null, "Hops are the flowers (also known as seed cones or strobiles) of the hop plant. They are primarily used as a flavoring and stability agent in beer, giving it a bitter, zesty, or citric flavor, but have additional medicinal and beverage uses. The hop plant is a vigorous climber that can grow to heights of 10m. It is trained to grow up trellises made from strings or wires.", new DateOnly(2000, 10, 31), new DateOnly(2000, 8, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b8139fe8d750003000418.jpg?1466663223", true, 36, 24, "Hops", "Transplant seedlings or rhizomes/rootstock", "Full Sun", "http://en.wikipedia.org/wiki/Hops" },
                    { new Guid("5b68f510-8937-4cac-ab6c-89e4306a16a9"), "Prunus", null, null, "The plum is a fruit of the subgenus Prunus of the genus Prunus. The Prunus genus also includes the cherry, apricot, almond, and peach. Within the subgenus Prunus, there are many species. The two largest species groups are European Plums (Prunus domestica) and Japanese Plums (Prunus salicina). Apricots are also classified as a section of the Prunus subgenus. The skin of plums can be coated with a waxy bloom or it can be shiny. Plums can be purple, green, yellow, or red. Shape varies from oval to globular. Plums can be dried to make prunes. European Plums require 800-900 hours of chilling during the winter, Japanese Plums require 300-500. Some varieties are self-pollinating, but all plum trees benefit from a pollination partner with the same bloom time within 15 meters. Standard and dwarf rootstocks are available. Dwarf trees can grow to 3 meters, standard to 4.5 meters. Depending on the size chosen, the tree will bear fruit within 3-6 years of planting.", new DateOnly(2000, 9, 30), new DateOnly(2000, 7, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/54b4af6c6130650002060000.jpg?1421127529", true, 60, 36, "Plum", "Transplant bare-root plant", "Full Sun", "https://en.wikipedia.org/wiki/Plum" },
                    { new Guid("6157cd2c-d468-42d0-9bce-874fd2d01211"), "Linum usitatissimum", 90, 120, "Flax is an annual, upright plant in the Linaceae family that was first cultivated in the Fertile Crescent for it's food and fibers. Flax is grown for it's seeds (which are eaten, ground into flaxmeal, added to bread, and pressed into oil), it's cellulose fibers (which are spun into linen), and as an ornamental. Flax does best in cooler climates, and can be direct seeded outdoors in the early spring as soon as the soil can be worked. Flax grows best in self-supporting patches, like grains, and should be broadcasted and raked. Seeds can be tossed with flour before sowing to help them spread evenly across the soil. Flax's slender stems grow quickly and produce pale blue flowers 15-25mm in diameter that mature into round seed pods containing 4-10 glossy brown seeds. The pods are left to dry on the plant until they turn from green to ochre or brown. Once 90% of the pods are dry, they can be harvested. Flax can also be grown as sprouts, which have a slightly spicy flavor. The seeds and oil are high in omega-3 fatty acids and are used as nutritional supplements. Flax fibers are two to three times as strong as cotton fibers and are naturally smooth and straight. Linseed meal, the byproduct from pressing oil from flax seeds, can be fed to livestock.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59e6a146a4beeb00040000cb.jpg?1508286787", false, null, null, "Flax", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Flax" },
                    { new Guid("6bb975a2-c71c-46cc-bd00-53554e8a2e99"), "Pyrus", null, null, "Pears are fruiting trees or shrubs in the Pyrus genus, in the family Rosaceae. There are approximately 3000 varieties worldwide, and they are commonly divided into two major groups: European and Asian. European Pears are wider at the bottom and taper at the top. Asian Pears are shaped more like apples, and have a different texture and flavor. Most pear species are deciduous, but some in southeast Asia are evergreen. Pears are generally cold-hardy, surviving winter temperatures between -25 to -40°C. In the spring, they produce white (and sometimes, yellow or pink) flowers with 5 petals. Like apples, pears are propagated by grafting the selected variety onto a rootstock. More growing information is available in individual pear species entries.", new DateOnly(2000, 10, 31), new DateOnly(2000, 8, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b8e47fe8d750003000439.jpg?1466666567", true, 84, 48, "Pear", "Transplant bare-root plant", "Full Sun", "https://en.wikipedia.org/wiki/Pear" },
                    { new Guid("6caaf46f-9c9e-4ef8-a1f1-c0a412003af4"), "Linum usitatissimum", 90, 120, "Flax is an annual, upright plant in the Linaceae family that was first cultivated in the Fertile Crescent for it's food and fibers. Flax is grown for it's seeds (which are eaten, ground into flaxmeal, added to bread, and pressed into oil), it's cellulose fibers (which are spun into linen), and as an ornamental. Flax does best in cooler climates, and can be direct seeded outdoors in the early spring as soon as the soil can be worked. Flax grows best in self-supporting patches, like grains, and should be broadcasted and raked. Seeds can be tossed with flour before sowing to help them spread evenly across the soil. Flax's slender stems grow quickly and produce pale blue flowers 15-25mm in diameter that mature into round seed pods containing 4-10 glossy brown seeds. The pods are left to dry on the plant until they turn from green to ochre or brown. Once 90% of the pods are dry, they can be harvested. Flax can also be grown as sprouts, which have a slightly spicy flavor. The seeds and oil are high in omega-3 fatty acids and are used as nutritional supplements. Flax fibers are two to three times as strong as cotton fibers and are naturally smooth and straight. Linseed meal, the byproduct from pressing oil from flax seeds, can be fed to livestock.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59e6a146a4beeb00040000cb.jpg?1508286787", false, null, null, "Flax", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Flax" },
                    { new Guid("6df8d9fd-c830-476f-b121-ca73898ebe88"), "Lactuca Sativa", 30, 60, "Lettuce is a cool weather crop and high temperatures will impede germination and/or cause the plant to bolt (go to seed quickly). Some hybrid cultivars have been bred to be more heat-resistant.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dcbb23732390003790100.jpg?1428016048", false, null, null, "Lettuce", "Direct seed outdoors, thin to 20cm when seedlings are 3cm tall", "Partial Sun", "https://en.wikipedia.org/wiki/Lettuce" },
                    { new Guid("7193b431-dbd9-4519-afd6-d4ce8c57949c"), "Solanum tuberosum", 70, 120, "Potatoes are starchy root vegetables in the Solanaceae, or Nightshade, family, which also includes tomatoes, eggplants, and peppers. They originated in South America, and spread to become a worldwide staple. The leaves and fruit are usually poisonous and the stem tuber is the only edible part once it is cooked. The potato can be cooked in many ways, brewed into alcohol, and also used as the basis for creating bioplastics. More growing information is available in individual species entries.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dbd5a3732390003600100.jpg?1428012376", false, null, null, "Potato", "Direct seed outdoors after last frost. Each piece must have one eye.", "Full Sun", "https://en.wikipedia.org/wiki/Potatoes" },
                    { new Guid("74a4018e-541c-45b5-9673-48b6500f6b8d"), "Armoracia rusticana", 150, 180, "Horseradish is a hardy perennial plant in the Brassica family (Brassicaceae) grown for it's tapered white taproot, generally 20-25cm long, which is used as a spice and eaten as a vegetable. Roots are ground or shredded to make pungent, spicy sauces and condiments. Seeds or root cuttings are planted in the fall, and the taproot is harvested the following year. It can be harvested in fall after the first frost kills off the greens, or in the early spring before new leaves grow. A winter of hard frosts will sweeten the root. Horseradish should be divided and replanted every 2 years to prevent it from becoming woody. The plant spreads rapidly. If invasive tendencies are managed properly, it is a good companion plant because it wards off diseases and repels insects like potato bugs and beetles, aphids, whiteflies, blister beetles, and some caterpillars.", null, null, "/assets/baren_field_square-4a827e5f09156962937eb100e4484f87e1e788f28a7c9daefe2a9297711a562a.jpg", false, null, null, "Horseradish", "Root cuttings", "Full Sun", "https://en.wikipedia.org/wiki/Horseradish" },
                    { new Guid("7520358d-c3d2-49d9-b411-c01704474341"), "Spinacia oleracea", 30, 50, "Spinach is an annual plant whose deep green leaves are eaten as a vegetable. It grows best in cooler weather. It can be eaten raw or cooked.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58c62a890a1f24000400000d.jpg?1489382019", false, null, null, "Spinach", "Direct seed outdoors, thin to 15cm when seedlings are 3cm high", "Full Sun", "https://en.wikipedia.org/wiki/Spinach" },
                    { new Guid("7676f03e-01bf-40b5-b9f7-07928b0a9b6c"), "Atriplex hortensis", 70, 100, null, null, null, null, false, null, null, "Orach", null, null, "https://en.wikipedia.org/wiki/Atriplex" },
                    { new Guid("77cd8525-5c96-4cff-9bd0-8ed6290eb3d1"), "Petroselinum crispum", 70, 90, "Parsley is an herb in the Apiaceae family with two main cultivars: flat leafed (or Italian) and curly. Some gardeners feel flat leaf is easier to cultivate since it is more tolerant of rain and sunshine. Curly parsley is more decorative in appearance. Both cultivars can be used fresh or dried to season food.", null, null, "/assets/baren_field_square-4a827e5f09156962937eb100e4484f87e1e788f28a7c9daefe2a9297711a562a.jpg", false, null, null, "Parsley", "direct seed indoors or outdoors", "Partial Sun", "https://en.wikipedia.org/wiki/Parsley" },
                    { new Guid("82cc4784-1d78-43db-9db5-bd8995177668"), "Zea mays", 60, 100, "Corn is a large grain plant, or tall grass, first domesticated about 10,000 years ago by indigenous peoples in Southern Mexico. The leafy stalk produces ears after pollination. Depending on the variety, the corn can be eaten fresh, or dried and ground into cornmeal.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b77b7fe8d750003000300.jpg?1466660787", false, null, null, "Corn", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Corn" },
                    { new Guid("89da926c-6cf0-45c0-b528-7816e83a6f5e"), "Cucurbita pepo", 50, 70, null, null, null, null, false, null, null, "Zucchini", null, null, "http://en.wikipedia.org/wiki/Zucchini" },
                    { new Guid("8a6295d7-83d9-4122-8c36-9bf8ae3c4330"), "Cucumis sativus", 50, 70, null, null, null, null, false, null, null, "Cucumber", null, null, "https://en.wikipedia.org/wiki/Cucumber" },
                    { new Guid("8cf0db68-269c-4422-82e8-e6eed0e6f123"), "Hordeum vulgare", 70, 120, "Barley is a member of the grass family that resembles wheat, but with more compact seeds. It is a major cereal grain that can be pearled, malted, or used as animal feed. Barley is often grown as a cover crop, and can be sown in spring or winter, depending on climate and desired use.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b73f4fe8d7500030002eb.jpg?1466659827", false, null, null, "Barley", "Direct seed using 15cm spacing, or broadcast and rake in", "Full Sun", "https://en.wikipedia.org/wiki/Barley" },
                    { new Guid("8d597668-ee9e-4cde-a164-c57995fb4730"), "beta vulgaris vulgaris", 50, 80, "Beetroot is also known as the beet, the golden beet, garden beet. \n\nIt's used in cooking, as well as coloring and medicine.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/54bf5bd76233390003000000.jpg?1421827030", false, null, null, "Beetroot", null, "Full Sun", "https://en.wikipedia.org/wiki/Beetroot" },
                    { new Guid("90aecdc7-599c-4a7c-b0dc-92081d1249eb"), "Helianthus annuus", 70, 100, "Sunflowers are large flowers with bright to deep yellow ray florets surrounding a large circular grouping of disc florets that mature into seeds. Sunflowers are grown for ornamental purposes, cut flowers, or their edible seeds. They can reach heights of 300cm or more.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/550c827f3730310003ea0000.jpg?1426883198", false, null, null, "Sunflower", "Direct seed outdoors after last frost in groups of 2-3, thin to 1 plant when true leaves appear", "Full Sun", "https://en.wikipedia.org/wiki/Sunflower" },
                    { new Guid("957fd962-5caf-4d13-b6c2-314158b6047e"), "Phaseolus vulgaris", 50, 100, null, null, null, null, false, null, null, "Bean", null, null, "https://en.wikipedia.org/wiki/Common_bean" },
                    { new Guid("a9a9d095-e126-454a-a481-244ba60b7e5e"), "Capsicum annuum", 60, 90, "The bell pepper is a cultivar group of the species Capsicum annuum. Bell pepper cultivars produce fruits in colors including red, yellow, orange, green, brown, white, and purple. The fruit is often mildly sweet, because this specific cultivar does not produce capsaicin, the chemical responsible for other peppers' spiciness.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/54b4b04f6130650002070000.jpg?1421127757", false, null, null, "Bell Pepper", "Purchased plants. Transplant when soil is warm.", "Full Sun", "http://en.wikipedia.org/wiki/Bell_pepper" },
                    { new Guid("ab6c51ec-df0a-4b13-87f4-ede96dca2a52"), "Castanea", null, null, "Chestnuts are a genus (Castanea) of eight or nine species of deciduous trees and shrubs in the beech family Fagaceae, which also includes oaks and beeches. They are native to temperate regions of the Northern Hemisphere. There are four main Chestnut species: European, Chinese, Japanese, and American. Horse chestnuts are in a separate genus, Aesculus, and produce similar-looking nuts that are mildly poisonous. Water chestnuts belong to the Cyperaceae family and are the tubers of an aquatic plant that taste similar to chestnuts. The four chestnut species produce trees that vary in size from shrubs to trees as tall as 60 meters. The European chestnut averages around 30 meters. Chinese and Japanese chestnuts tend to be wide-spreading, while European and American species are more consolidated. The American chestnut tree was largely destroyed by chestnut blight, a fungal disease, in the 20th century. The chestnut fruit grows in a spiny burr or cupule that splits open when the fruit is mature. The fruit has creamy white flesh and can germinate once the burr opens and falls to the ground. Unlike other nuts, chestnuts are high in carbohydrates rather than oil. They sweeten a few days after harvest as their starches begin to convert to sugar. Chestnuts are not self-fertile, and require another pollinator tree within 25 meters. Trees will bear fruit within 3-5 years of planting.", new DateOnly(2000, 10, 31), new DateOnly(2000, 9, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59d6758063be110004000000.jpg?1507227002", true, 120, 60, "Chestnut", "Direct seed or transplant sapling", "Full Sun", "https://en.wikipedia.org/wiki/Chestnut" },
                    { new Guid("ae459541-dbd6-43d7-971b-5acbaeafda98"), "Cynara cardunculus var. scolymus", 150, 200, "The globe artichoke is a variety of a species of thistle cultivated as a food. The budding artichoke flower-head is the edible part of the plant. It is a cluster of many budding small flowers (known as an \"inflorescence\") and bracts on an edible base. Once the buds bloom the head becomes coarse and barely edible. Artichokes are perennials in Zone 7 and warmer. They normally produce edible flower-heads during their second year, but recent cultivars such as 'Imperial Star' have been bred to produce in the first year. Other cultivars, such as 'Northern Star', have been bred to overwinter in more northern climates. There are green and purple varieties of artichoke. They are often steamed, sautéed or braised, but can also be eaten raw.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/580c9ee6ada34e000300009c.jpg?1477222118", false, null, null, "Artichoke", "Direct seed indoors. In warmer zones, propagate \"pups.\"", "Full Sun", "https://en.wikipedia.org/wiki/Artichoke" },
                    { new Guid("ae9df49f-daba-41cc-86d2-0f2c103f4e3e"), "Prunus armeniaca", null, null, "The apricot is a small fruiting tree in the genus Prunus (of which other stone fruits like peaches, plums and cherries are also members). It is a section of the Prunus (Plum) subgenus. The apricot tree has a dense, spreading canopy with white to pinkish flowers. It's fruit are yellow to orange, smaller than peaches, and have smooth or velvety skin. The apricot can tolerate winter temperatures down to ?30 °C, making it slightly more cold-hardy than peach trees. However, it is susceptible to spring frosts killing the blooms because it tends to flower very early. Some varieties are self-pollinating, but all apricot trees benefit from a pollination partner with the same bloom time within 15 meters. Standard and dwarf rootstocks are available. Dwarf trees can grow to 3 meters, standard to 4.5 meters. Depending on the size chosen, the tree will bear fruit within 2-4 years of planting.", new DateOnly(2000, 7, 31), new DateOnly(2000, 6, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5939e8f401f8790004000008.jpg?1496967407", true, 48, 24, "Apricot", "Transplant bare-root plant", "Full Sun", "https://en.wikipedia.org/wiki/Apricot" },
                    { new Guid("b1c7208d-f0b6-452f-b3bf-96245832506b"), "Apium graveolens", 120, 180, "The celery plant has long fibrous stalks that taper into leaves. The stalks and leaves can both be eaten. Celery seed is also used as a spice. Celery seed extracts are used in medicines.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b75b8fe8d7500030002f7.jpg?1466660278", false, null, null, "Celery", "Sow seeds indoors 10-12 weeks before transplanting outdoors", "Partial Sun", "https://en.wikipedia.org/wiki/Celery" },
                    { new Guid("b48f13c7-bff4-485c-9e5a-d2ffcb4347ba"), "Cydonia oblonga", null, null, "Quince is a deciduous tree in the Rosaceae family (which also includes apples and pears) that produces fruits of the same name that are apple or pear-shaped, 7-12 cm long, and golden-yellow when ripe. Immature fruit are green and covered in gray-white hair, most of which rubs off by the time the fruit is ripe. Quince can also be grown for ornamental purposes because of their striking white to red blossoms, twisted branches, and large leaves with fuzzy undersides. Some varieties are bred specifically for ornamental purposes and will not produce good fruit. Do not confuse Quince with Chinese Quince (Chaenomeles sinensis), which is a flowering ornamental that fruits poorly. Quince trees are cold-hardy to -23.3 °C and have a winter chilling requirement of 100-500 hours depending on the cultivar. Quince flowers are hermaphroditic and the trees are self-pollinating, but they will have higher fruit yields if another quince variety is planted nearby for cross-pollination. Fruit ripen on the tree in 5-6 months and snap easily from the branch when ripe. Quince will continue to ripen and soften after harvest. Ripe fruit are deeply fragrant but usually too hard and tart to eat fresh. When cooked, Quince has a slightly spicy, deeply rich flavor that pairs well with dried fruit or apples. Some varieties have been bred for fresh eating, and leaving the fruits on the tree through a frost is said to make them sweeter. Seeds are poisonous in large quantities. Transplanted saplings will fruit in three years.", new DateOnly(2000, 10, 31), new DateOnly(2000, 9, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5a135f0b956ccf000400000a.jpg?1511218943", true, 60, 36, "Quince", "Transplant grafted sapling or use cuttings", "Full Sun", "https://en.wikipedia.org/wiki/Quince" },
                    { new Guid("b61243e7-302f-4d75-8518-a33e24006ac8"), "Glycine max", 75, 120, "Soybeans are a type of legume that can be grown for many uses. Soybeans are one of the major crops grown in the United States. Like all legumes, they benefit from an inoculant at planting. They can be harvested fresh or dried. Soybeans are used to make oil, feed livestock, grown as cover crops, or processed into foods like tofu and tempeh. When harvested in an immature state and boiled whole in their pods, soybeans are known as Edamame.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b9050fe8d750003000442.jpg?1466667086", false, null, null, "Soybean", "Direct seed outdoors after last frost, thin to 30cm when seedlings are 4cm high", "Full Sun", "https://en.wikipedia.org/wiki/Soybean" },
                    { new Guid("ba26d40b-a537-4d11-a99d-00a74c6a74a9"), "Daucus Carota", 70, 80, "The carrot is a root vegetable. It is usually orange in color, but some cultivars are purple, black, red, white, and yellow. The most commonly eaten part of the plant is the taproot, but the greens are sometimes eaten as well. The leaves appear first, and the taproot grows more slowly beneath the soil. Fast-growing cultivars mature within three months of sowing the seed. Slower-maturing cultivars are harvested four months after sowing.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58c312395865650004000000.jpg?1489179191", false, null, null, "Carrot", "Direct Seed, thin to 3cm apart when seedlings are 8cm high", "Full Sun", "https://en.wikipedia.org/wiki/Carrot" },
                    { new Guid("bccb224e-f0b4-4b19-a257-981a3e52dbdd"), "Glycine max", 75, 120, "Soybeans are a type of legume that can be grown for many uses. Soybeans are one of the major crops grown in the United States. Like all legumes, they benefit from an inoculant at planting. They can be harvested fresh or dried. Soybeans are used to make oil, feed livestock, grown as cover crops, or processed into foods like tofu and tempeh. When harvested in an immature state and boiled whole in their pods, soybeans are known as Edamame.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b9050fe8d750003000442.jpg?1466667086", false, null, null, "Soybean", "Direct seed outdoors after last frost, thin to 30cm when seedlings are 4cm high", "Full Sun", "https://en.wikipedia.org/wiki/Soybean" },
                    { new Guid("be254d38-ea9d-49d2-93d8-eb4c6c781758"), "Brassica oleracea", 80, 120, "Cauliflower is a vegetable in the Brassicaceae family. The solid, firm head resembles that of broccoli and is usually white, but can also be yellow, purple, or green in color. Like broccoli, it sits atop a stalk. The head is wrapped in thick leaves that begin to open when the plant is ready for harvest. All cauliflower does best in cool weather.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58c3bde77b12f80004000010.jpg?1489223140", false, null, null, "Cauliflower", "Sow seeds indoors, harden seedlings off before transplanting", "Full Sun", "https://en.wikipedia.org/wiki/Cauliflower" },
                    { new Guid("c5fcb3b6-bb09-44a3-810b-e0ebb492cfd3"), "Oryza sativa", 90, 180, "Rice is the seed of the grass species Oryza sativa. It is a cereal grain that can come in many shapes, colors, and sizes. Rice requires ample water and is labor-intensive to cultivate. Water-controlling terrace systems enable it to be grown almost anywhere. The traditional method of cultivating rice is to flood the fields during or after setting the young seedlings. Rice can be grown without flooding, but flooding simplifies weed and pest control.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58e330a94bca6f00040000b8.jpg?1491284134", false, null, null, "Rice", "Seedlings", "Full Sun", "https://en.wikipedia.org/wiki/Rice" },
                    { new Guid("cc8f9043-fbff-4f88-a3d7-2b1298a49bf1"), "Fragaria × ananassa", null, null, "Strawberries are a hybrid species of the genus Fragaria that produce sweet, bright red fruits. \n\nThere are three main types of strawberries: \n\n1) summer-fruiting: produce a single, large crop of fruit the year after planting. To grow, transplant plugs or crowns in early spring in rows spaced at least 120cm apart. Pinch off all flowers the first season and train the plant's runners, pressing them into the soil 15-22cm apart from the mother plant. Mulch with straw or pine needles in the fall when the plants have died back. When the plants start to grow back in the spring, move the mulch aside. After harvest the second season, set a lawnmower to about 10cm high and mow, being sure not to damage crowns.\n\n The other two types are \n\n2) Ever-bearing and 3) Day Neutral, both of which send out less runners and bear several crops of smaller fruit throughout the season. These two types can be grown using raised beds about 20cm high and 60cm wide. Transplant crowns or plugs in staggered double rows, about 30cm apart. Remove runners and flowers until July of the first year to give the roots time to develop, and then allow plants to produce fruit. All types of strawberries begin to produce fewer and less sweet fruit once they are two years or older. Because strawberries are a hybrid, seeds will not breed true. Strawberries are predominantly propagated using bare root plugs or crowns or dividing runners. Make sure not to bury the crown when transplanting plugs.", new DateOnly(2000, 6, 30), new DateOnly(2000, 5, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dc6103732390003730100.jpg?1428014606", true, 6, 3, "Strawberry", "Transplant bare root plants/plugs or divide runners", "Full Sun", "https://en.wikipedia.org/wiki/Strawberry" },
                    { new Guid("cd9d7383-4ddc-4921-88d3-3499d612c54a"), "Brassica juncea", 60, 90, null, null, null, null, false, null, null, "Mustard", null, null, "https://en.wikipedia.org/wiki/Mustard_seed" },
                    { new Guid("cf6ddd91-d725-476c-805d-168c949d9590"), "Levisticum officinale", null, null, "Lovage is a perennial herb that tastes like an intensified cross between celery and parsley with a hint of anise. A member of Apiaceae family, it is related to carrots and celery. The leaves, roots, and young stems are edible. Leaves and hollow, thick stems can be added to salads, soups, and poultry dishes. The seeds can be used as a spice similar to fennel seeds. Lovage is also used as a companion crop because it's flowers attract beneficial insects like bees, hoverflies, lacewing larva, lady beetles, and parasitic wasps. Leaves can be harvested the first year, whole stems the second year.", new DateOnly(2000, 8, 31), new DateOnly(2000, 6, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59b171ef44d62e0004000000.jpg?1504801258", true, 36, 24, "Lovage", "Direct seed (indoors in early spring, outdoors in late spring or fall)", "Partial Sun", "https://en.wikipedia.org/wiki/Lovage" },
                    { new Guid("d06bf0eb-cd74-4733-a17d-fca7987725e6"), "Ribes nigrum", null, null, null, new DateOnly(2000, 7, 31), new DateOnly(2000, 6, 1), null, true, 36, 24, "Black Currant", null, null, "https://en.wikipedia.org/wiki/Ribes" },
                    { new Guid("d2bb5a5a-432a-47b5-b5c5-56075549935d"), "Prunus persica", null, null, "The peach tree is a deciduous tree native to Northwest China that produces stone, or drupe, fruits. It belongs to the genus Prunus which includes the cherry, apricot, almond, and plum. The peach is classified with the almond in the subgenus Amygdalus because their stones are corrugated rather than smooth. Peaches and nectarines are the same species - nectarines have a recessive gene that makes their skin smooth rather than fuzzy. Cultivated peaches are divided into two groups: clingstones and freestones, depending on whether the flesh sticks to the stone or not. Peaches can have white or yellow fuzzy skin. Yellow peaches usually have an acidic tang coupled with sweetness. White peaches are very sweet with little acidity. Most cultivars require 500 hours of chilling around 0 to 10 °C during the winter, and hot temperatures in the summer to ripen fruit. Some varieties are self-pollinating, while others require pollination by a peach tree of another variety with the same bloom period within 50 feet. Peaches should be thinned to 7-12cm apart when fruit are 2-3cm in diameter to increase mature fruit size. Standard and dwarf rootstocks are available. Dwarf trees can grow to 3 meters, standard to 4.5 meters. Depending on the size chosen, the tree will bear fruit within 2-4 years of planting.", new DateOnly(2000, 8, 31), new DateOnly(2000, 6, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59b1a6e444d62e0004000006.jpg?1504814816", true, 48, 24, "Peach", "Transplant bare-root plant", "Full Sun", "https://en.wikipedia.org/wiki/Peach" },
                    { new Guid("d5b313f8-b160-4b88-ba26-f6b397392747"), "Spinacia oleracea", 30, 50, "Spinach is an annual plant whose deep green leaves are eaten as a vegetable. It grows best in cooler weather. It can be eaten raw or cooked.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58c62a890a1f24000400000d.jpg?1489382019", false, null, null, "Spinach", "Direct seed outdoors, thin to 15cm when seedlings are 3cm high", "Full Sun", "https://en.wikipedia.org/wiki/Spinach" },
                    { new Guid("dae225fc-f1d3-4299-b9de-92cbcb45c458"), "Brassica oleracea (acephala)", 50, 80, "Kale is a cultivar of the species Brassica oleracea. It has green or purple leaves that branch off from one to multiple upright stems and do not form a head like cabbage. The plant is usually grown as an annual, but if grown as a perennial, it will form seeds in the second year. Current popular varieties include Curly kale, Italian kale, and Red Russian kale (green leaves with pale purple stems). It can be grown as baby salad greens or for bunching adult leaves. Leaves are sweeter after a frost and delicious eaten raw, added to salads, sautéed, or added to stews and casseroles.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/55b6b7ce6465380003910200.jpg?1438037945", false, null, null, "Kale", "Direct seed. If planting indoors, harden off before transplanting seedlings outside.", "Full Sun", "https://en.wikipedia.org/wiki/Kale" },
                    { new Guid("dc1b409e-ad35-435d-b857-630c175b171d"), "Brassica oleracea (acephala)", 50, 80, "Kale is a cultivar of the species Brassica oleracea. It has green or purple leaves that branch off from one to multiple upright stems and do not form a head like cabbage. The plant is usually grown as an annual, but if grown as a perennial, it will form seeds in the second year. Current popular varieties include Curly kale, Italian kale, and Red Russian kale (green leaves with pale purple stems). It can be grown as baby salad greens or for bunching adult leaves. Leaves are sweeter after a frost and delicious eaten raw, added to salads, sautéed, or added to stews and casseroles.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/55b6b7ce6465380003910200.jpg?1438037945", false, null, null, "Kale", "Direct seed. If planting indoors, harden off before transplanting seedlings outside.", "Full Sun", "https://en.wikipedia.org/wiki/Kale" },
                    { new Guid("dd059212-aa61-4e8b-a885-805066f23b0e"), "Pisum sativum", 50, 80, "Sweet, edible pods/seeds", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b8edefe8d75000300043c.jpg?1466666716", false, null, null, "Pea", "direct", "Full Sun", "https://en.wikipedia.org/wiki/Pea" },
                    { new Guid("e259cedf-fab8-412c-9822-2e9bfccb6bd3"), "Prunus cerasus", null, null, null, new DateOnly(2000, 7, 15), new DateOnly(2000, 6, 1), "/assets/baren_field_square-4a827e5f09156962937eb100e4484f87e1e788f28a7c9daefe2a9297711a562a.jpg", true, 60, 36, "Sour Cherry", null, null, "https://en.wikipedia.org/wiki/Prunus_cerasus" },
                    { new Guid("e2a83dc7-9111-480e-bf43-473f79ed2b04"), "Cicer arietinum", 90, 150, "Chickpeas are a cool-season annual legume in the Fabaceae family. They are native to the Middle East and Mediterranean. They can be sprouted indoors in 2-3 days for salads and fresh eating, or grown outside for their seeds. The upright plants do not require staking and grow to a height of about 50cm. They have small, feathery leaves. Flowers are white with blue, violet, or pink veins and form 2.5cm long oblong pods with 1-2 large, cream-colored seeds. Chickpeas can be picked while green and eaten fresh like snap peas, but they are most commonly harvested as a dried bean crop. Chickpeas are high in protein and drought-tolerant. They benefit from an inoculant at planting.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59d6761c63be110004000003.jpg?1507227163", false, null, null, "Chickpea", "Direct seed indoors, transplant seedlings outside after hardening off", "Full Sun", "https://en.wikipedia.org/wiki/Chickpea" },
                    { new Guid("e2b6d6a4-7dca-42c5-9bb0-a6d1cb4b9525"), "Brassica juncea", 60, 90, null, null, null, null, false, null, null, "Mustard", null, null, "https://en.wikipedia.org/wiki/Mustard_seed" },
                    { new Guid("e6e99e83-7c3e-42ac-ac0f-2a6db756024d"), "Cucumis melo", 70, 90, "Melons are a group of species in the Cucurbitaceae family. They produce sweet fruits that grow on sprawling vines. Types of melons include: watermelons, cantaloupe, honeydew, casaba, and crenshaw. More growing information is available in individual species entries.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b886afe8d750003000429.jpg?1466665064", false, null, null, "Melon", "Direct seed. If planting indoors, use peat pots and harden off before transplanting seedlings outside.", "Full Sun", "https://en.wikipedia.org/wiki/Melon" },
                    { new Guid("e7a9d7c9-e4db-487d-b57c-f8118565a366"), "Rheum rhabarbarum", null, null, "Rhubarb is a perennial herbaceous plant in USDA Zones 4-8. It cannot be harvested from in the first year of planting. It has large green leaves that are poisonous and thick, red-green stalks that are edible raw or cooked. It's stalks have a strong, tart taste and are commonly used in strawberry-rhubarb pies.", new DateOnly(2000, 6, 30), new DateOnly(2000, 4, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/593873176d9b0f00040000bf.jpg?1496871691", true, 36, 24, "Rhubarb", "Transplant crowns or direct seed indoors", "Full Sun", "https://en.wikipedia.org/wiki/Rhubarb" },
                    { new Guid("e7c7c44b-3474-4ca0-8a24-056f662e205f"), "Cannabis sativa", 70, 120, "Hemp is a variety of Cannabis sativa that is grown for industrial uses. It can be made into paper, textiles, clothing, rope, food, animal feed, biofuel, and biodegradable plastics. Hemp has a lower concentration of THC and a higher concentration of CBD than Marijuana (the other dominant variety of Cannabis sativa), which means it has little to no psychoactive effects. Despite this, the legality of hemp cultivation varies throughout the U.S. Hemp is low maintenance, fast growing, and an extremely efficient weed suppressor. It uses significantly less water and land than cotton to produce similar textile yields.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/59287947f9f0b200040000c2.jpg?1495824709", false, null, null, "Hemp", "Direct seed outdoors", "Full Sun", "https://en.wikipedia.org/wiki/Hemp" },
                    { new Guid("eb17f947-581d-4365-9ae9-4f7a3cd6bd90"), "Pastinaca sativa", 100, 150, "Parsnips are a root vegetable closely related to carrots and celery. They look like thick white carrots with larger, slightly rough foliage. Parsnips taste similar to carrots, except that they are sweeter, especially when cooked. Like carrots, they can be broiled, pureed, roasted, steamed, and baked into cakes. They become even sweeter in flavor after winter frosts. Before cane sugar arrived in Europe, they were used as a sweetener. The stems and foliage of the parsnip can cause a skin rash - wear long sleeves, pants, and gloves when weeding and harvesting.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/58c9bc91f577a40004000160.jpg?1489616014", false, null, null, "Parsnip", "Direct seed outdoors, thin seedlings to 5cm apart", "Full Sun", "https://en.wikipedia.org/wiki/Parsnip" },
                    { new Guid("edd52e3f-e467-4ad7-920d-6676ca3de4fd"), "Vaccinium sect. Cyanococcus", null, null, "Perennial flowering plants with sweet, indigo-colored berries. Blueberry plants are usually erect, prostrate shrubs that range in height from 10cm to 4m high, depending on the cultivar.", new DateOnly(2000, 8, 31), new DateOnly(2000, 6, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dc8023732390003760100.jpg?1428015100", true, 36, 24, "Blueberry", "Plant plants into soil or pots.", "Full Sun", "http://en.wikipedia.org/wiki/Blueberry" },
                    { new Guid("eedf39b9-7d75-4b37-a394-5bd1f5bd3da4"), "Hordeum vulgare", 70, 120, "Barley is a member of the grass family that resembles wheat, but with more compact seeds. It is a major cereal grain that can be pearled, malted, or used as animal feed. Barley is often grown as a cover crop, and can be sown in spring or winter, depending on climate and desired use.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b73f4fe8d7500030002eb.jpg?1466659827", false, null, null, "Barley", "Direct seed using 15cm spacing, or broadcast and rake in", "Full Sun", "https://en.wikipedia.org/wiki/Barley" },
                    { new Guid("f0e85866-a9c2-421d-a534-fc33f512661a"), "Solanum tuberosum", 70, 120, "Potatoes are starchy root vegetables in the Solanaceae, or Nightshade, family, which also includes tomatoes, eggplants, and peppers. They originated in South America, and spread to become a worldwide staple. The leaves and fruit are usually poisonous and the stem tuber is the only edible part once it is cooked. The potato can be cooked in many ways, brewed into alcohol, and also used as the basis for creating bioplastics. More growing information is available in individual species entries.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/551dbd5a3732390003600100.jpg?1428012376", false, null, null, "Potato", "Direct seed outdoors after last frost. Each piece must have one eye.", "Full Sun", "https://en.wikipedia.org/wiki/Potatoes" },
                    { new Guid("f5ebd275-1ed7-4bbd-b5ca-64bf8acd868f"), "Vaccinium ssp.", null, null, "Cranberries are perennial evergreen trailing vines or dwarf shrubs in the subgenus Oxycoccus of the genus Vaccinium that produce tart, red berries. Cranberries are native to North America. They bloom in late summer, with dark pink flowers, but the wiry stems are frost-sensitive. Cranberries are grown in peat bogs or specially constructed beds so that when temperatures drop, the frost-sensitive stems can be covered in water to help them tolerate cold better over the winter. Berries ripen from light green to red. Plants do not remain flooded throughout the year. Cranberries need 3 months of temperatures between 0-7° C. Common species include Vaccinium oxycoccos and Vaccinium macrocarpon. Plants will begin to bear fruit after 5 years and can live for 60-100 years.", new DateOnly(2000, 10, 31), new DateOnly(2000, 9, 1), "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/5a2ef2c2ec99880004000192.jpg?1513026235", true, 36, 24, "Cranberry", "Transplant vines or rooted seedlings", "Full Sun", "https://en.wikipedia.org/wiki/Cranberry" },
                    { new Guid("f6b7312d-0019-4e2d-9229-b3f625e5186f"), "Solanum melongena", 70, 90, "Eggplants commonly are egg-shaped with glossy black skin, but can come in a variety of other shapes and colors. They can be white, yellow, and pale to deep purple. Some are as small as goose eggs. The 'Rosa Bianca' cultivar is squat and round, while Asian cultivars can be long and thin. Eggplant stems are often spiny and their flowers range from white to purple. \n\nTheir flesh is generally white with a meaty texture and small seeds in the center. They are delicious grilled, roasted, in soups and stews, and breaded and fried.", null, null, "https://s3.amazonaws.com/openfarm-project/production/media/pictures/attachments/576b79ddfe8d75000300038a.jpg?1466661339", false, null, null, "Eggplant", "Sow seeds indoors and transplant out, or plant nursery seedlings", "Full Sun", "https://en.wikipedia.org/wiki/Eggplant" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("02734545-6762-4d3a-8474-4fb212dffe71"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("0314a200-03d1-4470-b806-2b3dba41b4f9"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("05736025-5fa0-46ac-b48a-f5903a779bd7"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("05cbba0c-7cf7-4f86-9457-6dafbe00c7cd"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("0695224d-7a00-4730-8786-c712113abbe5"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("0951aace-1a45-4f9c-bef2-a07e850a377a"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("0d0bb89a-9792-4d25-a7f0-f151a6fd77e6"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("0edbe025-32e4-4b69-b9d3-7bef157ae72f"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("0ee9c322-ce79-49e3-873f-1d0c437c1514"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("145472af-603c-4cf4-bbba-c7b557a1c8ce"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("20190095-7d39-4ef4-a687-5e98c09af0f7"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("20fc5890-3dd7-479a-be36-5502ce8f628b"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("283d5ae8-a13b-4289-bdbb-ace381d97925"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("2b6ef592-6181-47c0-9f05-fe0d04291671"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("33a3bde7-f50b-4f19-877e-264051eed218"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("35bfef24-94e1-4b92-b5fe-f4577bed893e"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("35f0f98b-f664-40b8-b881-86966c683105"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("3a887d21-6594-4d41-86c8-5abde19ceb5c"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("3d80ffef-3318-4596-8feb-36d98ee5fe38"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("40c8310c-3f72-47d1-bc06-4de6e818d0bc"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("41e53c61-8824-4e6b-8842-6f2e60567a60"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("44274b8e-c19f-49c6-9d32-db1356d2066d"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("4856d993-9d6c-4cea-94c6-bf6d5b71ad44"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("4ab704aa-7022-414b-a4fc-31b6e1b11ff6"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("4c028591-037a-4d2c-ae83-0240f634affa"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("4d1a2914-e84b-479b-a1b7-ea365aa8b73f"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("4e634558-3ec4-40bd-90ea-0354103c8ba1"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("508f17ec-2ce1-447c-8665-f326e0ce1f9a"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("512b47f4-20ab-4d22-9e47-af7d363f377f"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("52c5dffe-4732-4fb4-bc3c-5aa4d13811fa"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("57b1f5cd-5fe0-469a-a343-7f1a49c28e60"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("5b68f510-8937-4cac-ab6c-89e4306a16a9"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("6157cd2c-d468-42d0-9bce-874fd2d01211"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("6bb975a2-c71c-46cc-bd00-53554e8a2e99"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("6caaf46f-9c9e-4ef8-a1f1-c0a412003af4"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("6df8d9fd-c830-476f-b121-ca73898ebe88"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("7193b431-dbd9-4519-afd6-d4ce8c57949c"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("74a4018e-541c-45b5-9673-48b6500f6b8d"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("7520358d-c3d2-49d9-b411-c01704474341"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("7676f03e-01bf-40b5-b9f7-07928b0a9b6c"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("77cd8525-5c96-4cff-9bd0-8ed6290eb3d1"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("82cc4784-1d78-43db-9db5-bd8995177668"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("89da926c-6cf0-45c0-b528-7816e83a6f5e"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("8a6295d7-83d9-4122-8c36-9bf8ae3c4330"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("8cf0db68-269c-4422-82e8-e6eed0e6f123"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("8d597668-ee9e-4cde-a164-c57995fb4730"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("90aecdc7-599c-4a7c-b0dc-92081d1249eb"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("957fd962-5caf-4d13-b6c2-314158b6047e"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("a9a9d095-e126-454a-a481-244ba60b7e5e"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("ab6c51ec-df0a-4b13-87f4-ede96dca2a52"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("ae459541-dbd6-43d7-971b-5acbaeafda98"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("ae9df49f-daba-41cc-86d2-0f2c103f4e3e"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("b1c7208d-f0b6-452f-b3bf-96245832506b"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("b48f13c7-bff4-485c-9e5a-d2ffcb4347ba"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("b61243e7-302f-4d75-8518-a33e24006ac8"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("ba26d40b-a537-4d11-a99d-00a74c6a74a9"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("bccb224e-f0b4-4b19-a257-981a3e52dbdd"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("be254d38-ea9d-49d2-93d8-eb4c6c781758"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("c5fcb3b6-bb09-44a3-810b-e0ebb492cfd3"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("cc8f9043-fbff-4f88-a3d7-2b1298a49bf1"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("cd9d7383-4ddc-4921-88d3-3499d612c54a"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("cf6ddd91-d725-476c-805d-168c949d9590"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("d06bf0eb-cd74-4733-a17d-fca7987725e6"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("d2bb5a5a-432a-47b5-b5c5-56075549935d"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("d5b313f8-b160-4b88-ba26-f6b397392747"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("dae225fc-f1d3-4299-b9de-92cbcb45c458"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("dc1b409e-ad35-435d-b857-630c175b171d"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("dd059212-aa61-4e8b-a885-805066f23b0e"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("e259cedf-fab8-412c-9822-2e9bfccb6bd3"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("e2a83dc7-9111-480e-bf43-473f79ed2b04"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("e2b6d6a4-7dca-42c5-9bb0-a6d1cb4b9525"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("e6e99e83-7c3e-42ac-ac0f-2a6db756024d"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("e7a9d7c9-e4db-487d-b57c-f8118565a366"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("e7c7c44b-3474-4ca0-8a24-056f662e205f"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("eb17f947-581d-4365-9ae9-4f7a3cd6bd90"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("edd52e3f-e467-4ad7-920d-6676ca3de4fd"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("eedf39b9-7d75-4b37-a394-5bd1f5bd3da4"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("f0e85866-a9c2-421d-a534-fc33f512661a"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("f5ebd275-1ed7-4bbd-b5ca-64bf8acd868f"));

            migrationBuilder.DeleteData(
                table: "CropCatalogEntries",
                keyColumn: "Id",
                keyValue: new Guid("f6b7312d-0019-4e2d-9229-b3f625e5186f"));
        }
    }
}
