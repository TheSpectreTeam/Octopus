namespace Parser.FunctionalTests.ApiTests
{
    public class DynamicEntityEndpointsTests
    {
        private readonly Mock<IMongoRepository<ParserDynamicEntityModel>> _mockMongoRepository;

        public DynamicEntityEndpointsTests()
        {
            _mockMongoRepository = new Mock<IMongoRepository<ParserDynamicEntityModel>>();
        }

        #region GetAllEntitiesAsync

        [Fact]
        public async Task GetAllEntitiesAsync_GetEntitites_SuccessReturn()
        {
            //Arrange
            var dynamicEntities = new List<ParserDynamicEntityModel>
            {
                new ParserDynamicEntityModel
                {
                    Id = new string('0', 24),
                    EntityName = "TestDynamicEntity",
                    Properties = new List<DynamicEntityModelProperty>()
                },
                new ParserDynamicEntityModel
                {
                    Id = new string('1', 24),
                    EntityName = "TestDynamicEntity",
                    Properties = new List<DynamicEntityModelProperty>()
                },
            };

            _mockMongoRepository.Setup(_ => _.GetAllAsync(It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOptions>()))
                .ReturnsAsync(dynamicEntities);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act

            var response = await httpClient.GetAsync($"/api/parserDynamicEntityModel");
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Response<IEnumerable<ParserDynamicEntityModel>>>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            dynamicEntityResult?.Data.Should().BeEquivalentTo(dynamicEntities);
        }

        [Fact]
        public async Task GetAllEntitiesAsync_GetNotExistsEntitites_ReturnNoContext()
        {
            //Arrange
            _mockMongoRepository.Setup(_ => _.GetAllAsync(It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOptions>()))
                .ReturnsAsync(new List<ParserDynamicEntityModel>());

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/api/parserDynamicEntityModel");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region GetDynamicEntityById

        [Fact]
        public async Task GetDynamicEntityById_ReturnDynamicEntity_SuccessReturn()
        {
            //Arrange
            var id = new string('0', 24);
            var dynamicEntity = new ParserDynamicEntityModel
            {
                Id = id,
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>()
            };

            _mockMongoRepository.Setup(_ => _.GetByIdAsync(
                It.IsAny<object>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOptions>()))
                .ReturnsAsync(dynamicEntity);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            ////Act
            var response = await httpClient.GetAsync($"/api/parserDynamicEntityModel/{id}");
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Response<ParserDynamicEntityModel>>(responseText, serializeOptions);

            ////Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            dynamicEntityResult?.Data.Should().BeEquivalentTo(dynamicEntity);
        }

        [Fact]
        public async Task GetDynamicEntityById_ReturnNotExistsEntity_ReturnNotFound()
        {
            //Arrange
            var id = new string('0', 24);

            _mockMongoRepository.Setup(_ => _.GetByIdAsync(
                It.IsAny<object>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOptions>()))!
                .ReturnsAsync((ParserDynamicEntityModel?)null);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/api/parserDynamicEntityModel/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetDynamicEntityById_EnterInvalidId_ReturnValidationException()
        {
            //Arrange
            var id = new string('0', 3);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/api/parserDynamicEntityModel/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region AddNewEntityAsync

        [Fact]
        public async Task AddNewEntityAsync_AddNewEntity_Success()
        {
            //Arrange
            var id = new string('1', 24);

            var command = new CreateParserDynamicEntityModelCommand()
            {
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "propertyName",
                        SystemTypeName = "systemTypeName",
                        DatabaseEntityProperty = new DynamicEntityDatabaseProperty
                        {
                            DatabaseTypeName = "databaseTypeName"
                        }
                    }
                }
            };

            _mockMongoRepository.Setup(_ => _.CreateAsync(
                It.IsAny<ParserDynamicEntityModel>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/parserDynamicEntityModel", command);
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<string>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            dynamicEntityResult.Should().BeEquivalentTo(id);
        }

        [Fact]
        public async Task AddNewEntityAsync_AddInvalidEntity_ReturnValidationException()
        {
            //Arrange
            var command = new CreateParserDynamicEntityModelCommand();

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/parserDynamicEntityModel", command);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        #region AddNewEntitiesAsync

        [Fact]
        public async Task AddNewEntitiesAsync_AddNewEntities_Success()
        {
            //Arrange
            var ids = new Dictionary<int, object>()
            {
                { 0, new string('1', 24) },
                { 1, new string('2', 24) }
            };
                
            var command = new CreateManyParserDynamicEntityModelsCommand()
            {
                Models = new List<CreateParserDynamicEntityModelCommand>()
                {
                    new CreateParserDynamicEntityModelCommand()
                    {
                        EntityName = "TestDynamicEntity1",
                        Properties = new List<DynamicEntityModelProperty>
                        {
                            new DynamicEntityModelProperty
                            {
                                PropertyName = "propertyName",
                                SystemTypeName = "systemTypeName",
                                DatabaseEntityProperty = new DynamicEntityDatabaseProperty
                                {
                                    DatabaseTypeName = "databaseTypeName"
                                }
                            }
                        }
                    },
                    new CreateParserDynamicEntityModelCommand()
                    {
                        EntityName = "TestDynamicEntity2",
                        Properties = new List<DynamicEntityModelProperty>
                        {
                            new DynamicEntityModelProperty
                            {
                                PropertyName = "propertyName",
                                SystemTypeName = "systemTypeName",
                                DatabaseEntityProperty = new DynamicEntityDatabaseProperty
                                {
                                    DatabaseTypeName = "databaseTypeName"
                                }
                            }
                        }
                    },
                }
            };

            _mockMongoRepository.Setup(_ => _.CreateManyAsync(
                It.IsAny<List<ParserDynamicEntityModel>>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<InsertManyOptions>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(ids);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/parserDynamicEntityModels", command);
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Dictionary<int, object>>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            dynamicEntityResult[0].ToString().Should().BeEquivalentTo(ids[0].ToString());
            dynamicEntityResult[1].ToString().Should().BeEquivalentTo(ids[1].ToString());
        }

        [Fact]
        public async Task AddNewEntitiesAsync_AddInvalidEntities_ReturnValidationException()
        {
            //Arrange
            var command = new CreateManyParserDynamicEntityModelsCommand();

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PostAsJsonAsync("/api/parserDynamicEntityModels", command);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        #region UpdateEntityAsync

        [Fact]
        public async Task UpdateEntityAsync_UpdateEntity_SuccessUpdated()
        {
            //Arrange
            var id = new string('1', 24);
            var entity = new ParserDynamicEntityModel
            {
                Id = id,
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "propertyName",
                        SystemTypeName = "systemTypeName",
                        DatabaseEntityProperty = new DynamicEntityDatabaseProperty
                        {
                            DatabaseTypeName = "databaseTypeName"
                        }
                    }
                }
            };

            _mockMongoRepository.Setup(_ => _.ReplaceOneAsync(
                It.IsAny<ParserDynamicEntityModel>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOneAndReplaceOptions<ParserDynamicEntityModel, ParserDynamicEntityModel>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PutAsJsonAsync("/api/parserDynamicEntityModel", entity);
            var responseText = await response.Content.ReadAsStringAsync();
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var dynamicEntityResult = JsonSerializer.Deserialize<Response<ParserDynamicEntityModel>>(responseText, serializeOptions);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            dynamicEntityResult?.Data.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async Task UpdateEntityAsync_UpdateNotExistedEntity_SuccessUpdated()
        {
            //Arrange
            var id = new string('1', 24);

            var entity = new ParserDynamicEntityModel
            {
                Id = id,
                EntityName = "TestDynamicEntity",
                Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "propertyName",
                        SystemTypeName = "systemTypeName",
                        DatabaseEntityProperty = new DynamicEntityDatabaseProperty
                        {
                            DatabaseTypeName = "databaseTypeName"
                        }
                    }
                }
            };

            _mockMongoRepository.Setup(_ => _.ReplaceOneAsync(
                It.IsAny<ParserDynamicEntityModel>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<FindOneAndReplaceOptions<ParserDynamicEntityModel, ParserDynamicEntityModel>>(),
                It.IsAny<CancellationToken>()))!
                .ReturnsAsync((ParserDynamicEntityModel?)null);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PutAsJsonAsync("/api/parserDynamicEntityModel", entity);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task UpdateEntityAsync_UpdateByInvalidEntity_ReturnValidationException()
        {
            //Arrange
            var id = new string('1', 3);

            var entity = new ParserDynamicEntityModel
            {
                Id = id,
                EntityName = null,
                Properties = new List<DynamicEntityModelProperty>
                {
                    new DynamicEntityModelProperty
                    {
                        PropertyName = "propertyName",
                        SystemTypeName = "systemTypeName"
                    }
                }
            };

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.PutAsJsonAsync("/api/parserDynamicEntityModel", entity);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region DeleteEntityAsync

        [Fact]
        public async Task DeleteEntityAsync_DeleteEntity_SuccessDeleted()
        {
            //Arrange
            var id = new string('1', 24);

            _mockMongoRepository.Setup(_ => _.DeleteByIdAsync(
                It.IsAny<object>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync($"/api/parserDynamicEntityModel/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteEntityAsync_DeleteNotExistedEntity_ReturnNotFound()
        {
            //Arrange
            var id = new string('1', 24);

            _mockMongoRepository.Setup(_ => _.DeleteByIdAsync(
                It.IsAny<object>(),
                It.IsAny<MongoDatabaseSettings>(),
                It.IsAny<MongoCollectionSettings>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync($"/api/parserDynamicEntityModel/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteEntityAsync_DeleteByInvalidId_ReturnValidationException()
        {
            //Arrange
            var id = new string('1', 3);

            using var app = new TestParserApi(_ =>
            {
                _.AddSingleton(_mockMongoRepository.Object);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.DeleteAsync($"/api/parserDynamicEntityModel/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion
    }
}
