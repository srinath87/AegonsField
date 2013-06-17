// ===========================================================================
// Plane Mesh that Forms to the Terrain Profile script
// written by Jason Kowalin ( Alucard J , Jay Kay )
// April 2013
// ===========================================================================
 
#pragma strict
@script RequireComponent( MeshFilter )
@script RequireComponent( MeshRenderer )
 
// --------------------------------------------------------------------------- Terrain Data
 
public var terrain : Terrain;
private var terrainData : TerrainData;
private var terrainSize : Vector3;
private var heightmapWidth : int;
private var heightmapHeight : int;
private var heightmapData : float[,];
 
 
function GetTerrainData()
{
if ( !terrain )
{
terrain = Terrain.activeTerrain;
}
 
terrainData = terrain.terrainData;
 
terrainSize = terrain.terrainData.size;
 
heightmapWidth = terrain.terrainData.heightmapWidth;
heightmapHeight = terrain.terrainData.heightmapHeight;
 
heightmapData = terrainData.GetHeights( 0, 0, heightmapWidth, heightmapHeight );
}
 
 
// --------------------------------------------------------------------------- Raycast To Terrain
 
private var rayHitPoint : Vector3;
private var heightmapPos : Vector3;
 
 
function Start()
{
GetTerrainData();
ConstructMesh();
}
 
 
function Update()
{
// raycast to the terrain
RaycastToTerrain();
 
// find the heightmap position of that hit
GetHeightmapPosition();
 
// Calculate Grid
CalculateGrid();
 
// Update Mesh
UpdateMesh();
}
 
 
function RaycastToTerrain()
{
var hit : RaycastHit;
var rayPos : Ray = Camera.main.ScreenPointToRay( Input.mousePosition );
 
if ( Physics.Raycast( rayPos, hit, Mathf.Infinity ) ) // also consider a layermask to just the terrain layer
{
Debug.DrawLine( Camera.main.transform.position, hit.point, Color.red );
rayHitPoint = hit.point;
}
}
 
 
function GetHeightmapPosition()
{
// find the heightmap position of that hit
heightmapPos.x = ( rayHitPoint.x / terrainSize.x ) * parseFloat( heightmapWidth );
heightmapPos.z = ( rayHitPoint.z / terrainSize.z ) * parseFloat( heightmapHeight );
 
// convert to integer
heightmapPos.x = Mathf.Round( heightmapPos.x );
heightmapPos.z = Mathf.Round( heightmapPos.z );
 
// clamp to heightmap dimensions to avoid errors
heightmapPos.x = Mathf.Clamp( heightmapPos.x, 0, heightmapWidth - 1 );
heightmapPos.z = Mathf.Clamp( heightmapPos.z, 0, heightmapHeight - 1 );
}
 
 
// --------------------------------------------------------------------------- Calculate Grid
 
private var mapGrid : Vector3[,] = new Vector3[ 9, 9 ];
 
public var indicatorSize : float = 1.0;
public var indicatorOffsetY : float = 5.0;
 
 
function CalculateGrid()
{
for ( var z : int = -4; z < 5; z ++ )
{
for ( var x : int = -4; x < 5; x ++ )
{
var calcVector : Vector3;
 
calcVector.x = heightmapPos.x + ( x * indicatorSize );
calcVector.x /= parseFloat( heightmapWidth );
calcVector.x *= terrainSize.x;
 
var calcPosX : float = heightmapPos.x + ( x * indicatorSize );
calcPosX = Mathf.Clamp( calcPosX, 0, heightmapWidth - 1 );
 
var calcPosZ : float = heightmapPos.z + ( z * indicatorSize );
calcPosZ = Mathf.Clamp( calcPosZ, 0, heightmapHeight - 1 );
 
calcVector.y = heightmapData[ calcPosZ, calcPosX ] * terrainSize.y; // heightmapData is Y,X ; not X,Y (reversed)
calcVector.y += indicatorOffsetY; // raise slightly above terrain
 
calcVector.z = heightmapPos.z + ( z * indicatorSize );
calcVector.z /= parseFloat( heightmapHeight );
calcVector.z *= terrainSize.z;
 
mapGrid[ x + 4, z + 4 ] = calcVector;
}
}
}
 
 
// --------------------------------------------------------------------------- INDICATOR MESH
 
private var mesh : Mesh;
 
private var verts : Vector3[];
private var uvs : Vector2[];
private var tris : int[];
 
 
function ConstructMesh()
{
if ( !mesh )
{
mesh = new Mesh();
GetComponent(MeshFilter).mesh = mesh;
mesh.name = gameObject.name + "Mesh";
}
 
mesh.Clear();
 
verts = new Vector3[9 * 9];
uvs = new Vector2[9 * 9];
tris = new int[ 8 * 2 * 8 * 3];
 
var uvStep : float = 1.0 / 8.0;
 
var index : int = 0;
var triIndex : int = 0;
 
for ( var z : int = 0; z < 9; z ++ )
{
for ( var x : int = 0; x < 9; x ++ )
{
verts[ index ] = new Vector3( x, 0, z );
uvs[ index ] = new Vector2( parseFloat(x) * uvStep, parseFloat(z) * uvStep );
 
if ( x < 8 && z < 8 )
{
tris[ triIndex + 0 ] = index + 0;
tris[ triIndex + 1 ] = index + 9;
tris[ triIndex + 2 ] = index + 1;
 
tris[ triIndex + 3 ] = index + 1;
tris[ triIndex + 4 ] = index + 9;
tris[ triIndex + 5 ] = index + 10;
 
triIndex += 6;
}
 
index ++;
}
}
 
 
// - Build Mesh -
mesh.vertices = verts;
mesh.uv = uvs;
mesh.triangles = tris;
 
mesh.RecalculateBounds();
mesh.RecalculateNormals();
}
 
 
function UpdateMesh()
{
var index : int = 0;
 
for ( var z : int = 0; z < 9; z ++ )
{
for ( var x : int = 0; x < 9; x ++ )
{
verts[ index ] = mapGrid[ x, z ];
 
index ++;
}
}
 
// assign to mesh
mesh.vertices = verts;
 
mesh.RecalculateBounds();
mesh.RecalculateNormals();
}
 
 