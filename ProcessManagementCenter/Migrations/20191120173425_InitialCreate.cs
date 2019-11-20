using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagementCenter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MineArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MineArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinerMode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinerMode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinerOperation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinerOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinerStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinerStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinerType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CodeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsProductionShift = table.Column<bool>(nullable: false),
                    TimeToFirstOperate = table.Column<DateTime>(nullable: false),
                    TimeToLastOperate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Miners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    WorkUnitId = table.Column<int>(nullable: true),
                    ShiftId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Miners_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Miners_MinerType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MinerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Miners_WorkUnit_WorkUnitId",
                        column: x => x.WorkUnitId,
                        principalTable: "WorkUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinerState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortOrder = table.Column<int>(nullable: false),
                    CurrentWork = table.Column<int>(nullable: false),
                    TargetWork = table.Column<int>(nullable: false),
                    OperatingRate = table.Column<float>(nullable: false),
                    CutDepth = table.Column<float>(nullable: false),
                    EosProjection = table.Column<float>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    AreaId = table.Column<int>(nullable: true),
                    OperationId = table.Column<int>(nullable: true),
                    ModeId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    MinerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinerState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinerState_MineArea_AreaId",
                        column: x => x.AreaId,
                        principalTable: "MineArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinerState_Miners_MinerId",
                        column: x => x.MinerId,
                        principalTable: "Miners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MinerState_MinerMode_ModeId",
                        column: x => x.ModeId,
                        principalTable: "MinerMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinerState_MinerOperation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "MinerOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinerState_MinerStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "MinerStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MethaneState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Value = table.Column<float>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    MinerStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethaneState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethaneState_MinerState_MinerStateId",
                        column: x => x.MinerStateId,
                        principalTable: "MinerState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethaneState_MinerStateId",
                table: "MethaneState",
                column: "MinerStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Miners_ShiftId",
                table: "Miners",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Miners_TypeId",
                table: "Miners",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Miners_WorkUnitId",
                table: "Miners",
                column: "WorkUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MinerState_AreaId",
                table: "MinerState",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MinerState_MinerId",
                table: "MinerState",
                column: "MinerId");

            migrationBuilder.CreateIndex(
                name: "IX_MinerState_ModeId",
                table: "MinerState",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_MinerState_OperationId",
                table: "MinerState",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_MinerState_StatusId",
                table: "MinerState",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MethaneState");

            migrationBuilder.DropTable(
                name: "MinerState");

            migrationBuilder.DropTable(
                name: "MineArea");

            migrationBuilder.DropTable(
                name: "Miners");

            migrationBuilder.DropTable(
                name: "MinerMode");

            migrationBuilder.DropTable(
                name: "MinerOperation");

            migrationBuilder.DropTable(
                name: "MinerStatus");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "MinerType");

            migrationBuilder.DropTable(
                name: "WorkUnit");
        }
    }
}
