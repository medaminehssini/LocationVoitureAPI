using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LocationVoitureApi.Migrations
{
    public partial class mydb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mydb");

            migrationBuilder.CreateTable(
                name: "admin",
                schema: "mydb",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    email = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    password = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    photo = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agence",
                schema: "mydb",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    adresse = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agence", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                schema: "mydb",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    prenom = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    cin = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    adresse = table.Column<string>(type: "text", nullable: true),
                    telephone = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    email = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "marque",
                schema: "mydb",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marque", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employeur",
                schema: "mydb",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    photo = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    password = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    idagence = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeur", x => x.id);
                    table.ForeignKey(
                        name: "employeur$fk_employeur_agence1",
                        column: x => x.idagence,
                        principalSchema: "mydb",
                        principalTable: "agence",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "voiture",
                schema: "mydb",
                columns: table => new
                {
                    matricule = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    poids = table.Column<int>(type: "integer", nullable: true),
                    prix = table.Column<decimal>(type: "numeric(18,4)", nullable: true),
                    modele = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    type = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    photo = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    idmarque = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voiture", x => x.matricule);
                    table.ForeignKey(
                        name: "FK_voiture_marque",
                        column: x => x.idmarque,
                        principalSchema: "mydb",
                        principalTable: "marque",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "location",
                schema: "mydb",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_deb = table.Column<DateTime>(type: "date", nullable: true),
                    date_fin = table.Column<DateTime>(type: "date", nullable: true),
                    idClient = table.Column<int>(type: "integer", nullable: false),
                    voiture_matricule = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    idemployeur = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.id);
                    table.ForeignKey(
                        name: "FK_location_employeur",
                        column: x => x.idemployeur,
                        principalSchema: "mydb",
                        principalTable: "employeur",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_location_voiture",
                        column: x => x.voiture_matricule,
                        principalSchema: "mydb",
                        principalTable: "voiture",
                        principalColumn: "matricule");
                    table.ForeignKey(
                        name: "location$fk_location_Client1",
                        column: x => x.idClient,
                        principalSchema: "mydb",
                        principalTable: "client",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "client$cin_UNIQUE",
                schema: "mydb",
                table: "client",
                column: "cin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_employeur_agence1_idx",
                schema: "mydb",
                table: "employeur",
                column: "idagence");

            migrationBuilder.CreateIndex(
                name: "fk_location_Client1_idx",
                schema: "mydb",
                table: "location",
                column: "idClient");

            migrationBuilder.CreateIndex(
                name: "fk_location_employeur1_idx",
                schema: "mydb",
                table: "location",
                column: "idemployeur");

            migrationBuilder.CreateIndex(
                name: "fk_location_voiture1_idx",
                schema: "mydb",
                table: "location",
                column: "voiture_matricule");

            migrationBuilder.CreateIndex(
                name: "fk_voiture_marque_idx",
                schema: "mydb",
                table: "voiture",
                column: "idmarque");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "location",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "employeur",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "voiture",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "client",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "agence",
                schema: "mydb");

            migrationBuilder.DropTable(
                name: "marque",
                schema: "mydb");
        }
    }
}
